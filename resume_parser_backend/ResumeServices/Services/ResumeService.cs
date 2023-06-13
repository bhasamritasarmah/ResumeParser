using ceTe.DynamicPDF.Rasterizer;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using ResumeServices;
using ResumeServices.Models;
using System.IO;


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

        public void UploadResume(IFormFile resume)
        {
            if (resume.Length > 0)
            {
                byte[] pdfBytes;
                string output = resume.FileName;
                output = output.Substring(0, output.IndexOf('.'));
                string outputFile = "D:\\" + output;

                using (var ms = new MemoryStream())
                {
                    resume.CopyToAsync(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    //var id = _bucket.UploadFromStream(resume.FileName, ms);
                    pdfBytes = ms.ToArray();
                }

                InputPdf inputPdf = new InputPdf(pdfBytes);
                PdfRasterizer rasterizer = new PdfRasterizer(inputPdf);
                byte[] tiffBytes = rasterizer.DrawToMultiPageTiff(ImageFormat.TiffWithLzw, ImageSize.Dpi96);

                File.WriteAllBytes(outputFile + ".tiff", tiffBytes);
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
