using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ResumeServices;

namespace ResumeUploadAndDisplayBackend.Services
{
    public class ResumeService : IResumeService
    {
        private readonly GridFSBucket _bucket;
        public ResumeService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bucket = new GridFSBucket(database);
        }

        public async void UploadResume (IFormFile resume)
        {
            using (var ms = new MemoryStream())
            {
                await resume.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var id = _bucket.UploadFromStreamAsync(resume.FileName, ms);
            }
        }
    }
}
