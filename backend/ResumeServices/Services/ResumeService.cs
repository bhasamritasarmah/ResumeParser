using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ResumeServices;
using ResumeServices.Models;
using MongoDB.Bson;
using Newtonsoft.Json;

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
        /// <returns>A string message saying whether it was successful or or failure.</returns>
        public async Task<string> UploadAndParse(IFormFile resume)
        {
            try
            {
                if (resume != null && resume.Length > 0)
                {
                    string id;
                    using (var ms = new MemoryStream())
                    {
                        resume.CopyTo(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        id = (await _bucket.UploadFromStreamAsync(resume.FileName, ms)).ToString();
                    }

                    using (var stream = resume.OpenReadStream())
                    {
                        var content = new MultipartFormDataContent();
                        content.Add(new StreamContent(stream), "resume", resume.FileName);
                        content.Add(new StringContent(id), "resume_id");

                        var client = new HttpClient();
                        var response = await client.PostAsync("http://127.0.0.1:5000/upload", content);

                        if (response.IsSuccessStatusCode)
                        {
                            var parsed_json = await response.Content.ReadAsStringAsync();
                            Resume parsed_resume = JsonConvert.DeserializeObject<Resume>(parsed_json);
                            _collection.InsertOne(parsed_resume);
                        }
                        else
                        {
                            return "Error parsing the resume. Please try again.";
                        }
                    }
                }
                else
                {
                    return "No resume file provided.";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }

            return "Resume uploaded and parsed successfully.";
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
        /// Accepts a MongoDB collection id and produces the corresponding File for download.
        /// </summary>
        /// <param name="id">A string id.</param>
        /// <returns>The File stream, and the corresponding filename.</returns>
        public (Stream, string) GetResume (string id)
        {
            var objectId = new ObjectId(id);
            
            var resumeFile = _bucket.Find(Builders<GridFSFileInfo>.Filter.Eq("_id", objectId)).FirstOrDefault();

            if (resumeFile == null)
            {
                return (null, null);
            }

            var resumeStream = _bucket.OpenDownloadStream(objectId);

            return (resumeStream, resumeFile.Filename);
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
