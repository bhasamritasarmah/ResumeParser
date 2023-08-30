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

        /// <summary>
        /// The 'ResumeController' uses the interface 'IResumeService' to
        /// perform tasks like uploading files to the database, displaying
        /// the list of files from the database, and displaying the details
        /// of a particular file from the database whose 'id' has been provided.
        /// </summary>
        /// <param name="service"></param>
        public ResumeController(IResumeService service)
        {
            _service = service;
        }

        //POST api/<ResumeController>
        /// <summary>
        /// The 'Post' method is used to upload a resume to the database
        /// with the help of a GridFS Bucket. The parameter name of this
        /// method should match the key name of the formData.append method
        /// in the React frontend file.
        /// </summary>
        /// <param name="resume"></param>
        [HttpPost]
        public async Task<string> Post (IFormFile resume)
        {
            return await _service.UploadAndParse(resume);
        }

        /// <summary>
        /// The 'ListResumes' method is used to display the list of
        /// resumes which are uploaded to the database and have already
        /// been parsed. This method, as of now, has not been implemented.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Resume> ListResumes ()
        {
            return _service.GetAll();
        }

        [HttpGet]
        public IActionResult DownloadResume (string id)
        {
            var (resumeStream, fileName) = _service.GetResume(id);
            
            if (resumeStream == null)
            {
                return NotFound();
            }

            string contentType = "multipart/form-data";

            return File(resumeStream, contentType, fileName);
        }
        
        /// <summary>
        /// The 'ResumeDetails' method takes in the id of the resume
        /// been selected from the list of resumes displayed and 
        /// displays the details of that particular resume in a JSON format.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Resume ResumeDetails (string id)
        {
            return _service.GetDetails(id);
        }
    }
}
