using Microsoft.AspNetCore.Http;
using ResumeServices.Models;

namespace ResumeUploadAndDisplayBackend.Services
{
    public interface IResumeService
    {
        Task UploadResume(IFormFile resume);
        List<Resume> GetResumes();
        Resume GetResumeDetails(string id);
    }
}
