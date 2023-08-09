using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ResumeServices;
using ResumeServices.Models;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace ResumeUploadAndDisplayBackend.Services
{
    public class ResumeService : IResumeService
    {
        private readonly GridFSBucket _bucket;
        private readonly IMongoCollection<Resume> _collection;

        /// <summary>
        /// The 'ResumeService' class makes use of the interface 'IDatabaseSettings'
        /// to get the database connection string, database name, and
        /// collection name. The interface IMongoClient is used to connect to those
        /// particular database and collection in the MongoDB database system.  
        /// As of now, the collection name is not being used.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="mongoClient"></param>
        public ResumeService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bucket = new GridFSBucket(database);
            _collection = database.GetCollection<Resume>(settings.CollectionName);
        }


        /// <summary>
        /// The method 'UploadandParse' takes in a file as an IFormFile object
        /// and uploads the whole object in the memory stream. From there, it
        /// is stored into the database with the help of a GridFS Bucket.
        /// GridFS returns and id, which is converted to string. The resume is then
        /// converted to MultipartFormData and sent to Python API, which parses the 
        /// resume and returns the details in a JSON string. The id received from 
        /// GridFS is added to this JSON string and the whole JSON object is stored
        /// in the database.
        /// </summary>
        /// <param name="resume"></param>
        public async Task UploadAndParse(IFormFile resume)
        {
            if (resume != null && resume.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    resume.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);  // Once the whole object is copied to the
                                                   // memory stream, the pointer points to the 
                                                   // end of the stream. In order to upload the 
                                                   // stream to the database, the pointer must 
                                                   // be brought back to the starting of the 
                                                   // stream or otherwise the database storage 
                                                   // will remain empty.
                    var id = (await _bucket.UploadFromStreamAsync(resume.FileName, ms)).ToString();

                    var connect_id = new ConnectId   //Creating a JSON string for resume_id here.
                    {
                        resume_id = id
                    };

                    using (var stream = resume.OpenReadStream())
                    {
                        //The resume is sent to the python API in the form of MultipartFormData
                        var content = new MultipartFormDataContent();
                        content.Add(new StreamContent(stream), "resume", resume.FileName);

                        var client = new HttpClient();
                        //Client timeout is increased since the LLM model takes some time to run.
                        client.Timeout = TimeSpan.FromMinutes(15);
                        var response = await client.PostAsync(
                            "http://d84a-35-243-131-48.ngrok-free.app/uploader", content);

                        if (response.IsSuccessStatusCode)   //In case the API is active...
                        {
                            //jsonString2 is the JSON string received from python API.
                            var jsonString2 = await response.Content.ReadAsStringAsync();
                            //jsonString1 contains resume_id in a JSON string.
                            var jsonString1 = JsonSerializer.Serialize(connect_id);

                            //Both the above JSON strings are converted to JSON objects,
                            //merged into one and converted back to JSON string.
                            var json1 = JObject.Parse(jsonString1);
                            var json2 = JObject.Parse(jsonString2);
                            var jsonObject = new JObject();
                            jsonObject.Merge(json1);
                            jsonObject.Merge(json2);
                            string jsonString = jsonObject.ToString();

                            //The JSON string is stored as a 'Resume' object and 
                            //stored into the database.
                            Resume jsonElement = JsonSerializer.Deserialize<Resume>(jsonString);
                            await _collection.InsertOneAsync(jsonElement);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The method 'GetAll' gets a list of all the resumes which were uploaded
        /// into the database and also have already been parsed into JSON structures.
        /// </summary>
        /// <returns></returns>
        public List<Resume> GetAll()
        {
            return _collection.Find(p => true).ToList();
        }

        /// <summary>
        /// The method 'GetDetails' finds the resume with the given 'id'
        /// and displays the details of that particular resume in a JSON format.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Resume GetDetails(string id)
        {
            return _collection.Find(p => p.Id == id).FirstOrDefault();
        }
    }
}
