using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeServices.Models;
using ResumeUploadAndDisplayBackend.Services;

namespace ResumeParserBackendAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IResumeService _service;

        public ResumeController(IResumeService service)
        {
            _service = service;
        }

        //POST api/<ResumeController>
        [HttpPost]
        public async Task Post (IFormFile resume)
        {
            await _service.UploadResume(resume);
        }

        [HttpGet]
        public List<Resume> ListResumes ()
        {
            return _service.GetResumes();
        }
        
        [HttpGet]
        public Resume ResumeDetails (string id)
        {
            return _service.GetResumeDetails(id);
        }
    }
}
