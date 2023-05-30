using Microsoft.AspNetCore.Http;

namespace ResumeUploadAndDisplayBackend.Services
{
    public interface IResumeService
    {
        void UploadResume(IFormFile resume);
    }
}
