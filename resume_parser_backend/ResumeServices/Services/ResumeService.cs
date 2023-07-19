using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ResumeServices;
using ResumeServices.Models;


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
        /// The method 'UploadResume' takes in a file as an IFormFile object
        /// and uploads the whole object in the memory stream. From there, it
        /// is stored into the database with the help of a GridFS Bucket.
        /// </summary>
        /// <param name="resume"></param>
        public void UploadResume(IFormFile resume)
        {
            if (resume.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    resume.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);  // Once the whole object is copied to the
                                                   // memory stream, the pointer points to the 
                                                   // end of the stream. In order to upload the 
                                                   // stream to the database, the pointer must be 
                                                   // brought back to the starting of the stream 
                                                   // or otherwise the database storage will 
                                                   // remain empty.
                    var id = _bucket.UploadFromStream(resume.FileName, ms);
                }
            }
        }

        /// <summary>
        /// The method 'GetResumes' gets a list of all the resumes which were uploaded
        /// into the database and also have already been parsed into JSON structures.
        /// </summary>
        /// <returns></returns>
        public List<Resume> GetResumes()
        {
            return _collection.Find(p => true).ToList();
        }

        /// <summary>
        /// The method 'GetResumeDetails' finds the resume with the given 'id'
        /// and displays the details of that particular resume in a JSON format.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Resume GetResumeDetails(string id)
        {
            return _collection.Find(p => p.Id == id).FirstOrDefault();
        }
    }
}
