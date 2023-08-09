using Microsoft.AspNetCore.Http;
using ResumeServices.Models;

namespace ResumeUploadAndDisplayBackend.Services
{
    /// <summary>
    /// The 'IResumeService' interface contains the method declarations
    /// for all those tasks that are to be carried out by the controller,
    /// using dependency injection. The definitions for these methods
    /// are written in the corresponding 'ResumeService' file.
    /// </summary>
    public interface IResumeService
    {
        Task UploadAndParse(IFormFile resume);
        List<Resume> GetAll();
        Resume GetDetails(string id);
    }
}
