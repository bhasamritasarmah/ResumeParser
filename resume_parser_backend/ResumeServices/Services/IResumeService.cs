using Microsoft.AspNetCore.Http;
using ResumeServices.Models;

namespace ResumeUploadAndDisplayBackend.Services
{
    public interface IResumeService
    {
        void UploadResume(IFormFile resume);
        List<Resume> GetResumes();
        Resume GetResumeDetails(string id);
    }
}
