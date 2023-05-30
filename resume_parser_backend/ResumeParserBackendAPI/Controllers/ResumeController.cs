using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResumeUploadAndDisplayBackend.Services;

namespace ResumeParserBackendAPI.Controllers
{
    [Route("api/[controller]")]
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
        public void Post (IFormFile resume)
        {
            _service.UploadResume(resume);
        }
    }
}
