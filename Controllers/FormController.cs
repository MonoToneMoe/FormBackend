using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly  FormService _service;

        public FormController(FormService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("AddForm")]
        public IActionResult UploadForm([FromBody] FormModel form) => _service.NewForm(form) ? Ok("Successfully created") : BadRequest("Could not create Form");

        [HttpGet]
        [Route("GetAllForms")]
        public IEnumerable<FormModel> GetForms() => _service.GetForms();

        [HttpDelete]
        [Route("DeleteForm/{id}")]
        public IActionResult DeleteForm(int id) => _service.DeleteForm(id) ? Ok() : BadRequest("Form not found");

        [HttpPost]
        [Route("EditForm")]
        public IActionResult EditForm([FromBody] FormModel form) => _service.EditForm(form) ? Ok("Successfully updated") : BadRequest();
    }
}