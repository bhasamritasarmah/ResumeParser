using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ResumeServices;
using ResumeServices.Models;
using System.ComponentModel;

namespace ResumeUploadAndDisplayBackend.Services
{
    public class ResumeService : IResumeService
    {
        private readonly GridFSBucket _bucket;
        private readonly IMongoCollection<Resume> _collection;

        public ResumeService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bucket = new GridFSBucket(database);
            _collection = database.GetCollection<Resume>(settings.CollectionName);
        }

        public async void UploadResume (IFormFile resume)
        {
            if (resume.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await resume.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    var id = _bucket.UploadFromStreamAsync(resume.FileName, ms);
                }
            }
        }

        public List<Resume> GetResumes()
        {
            return _collection.Find(p => true).ToList();
        }

        public Resume GetResumeDetails(string id)
        {
            return _collection.Find(p => p.Id == id).FirstOrDefault();
        }
    }
}
