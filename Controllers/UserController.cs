using FormBackend.Model.DTOS;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class UserController(UserService userService) : ControllerBase{
        private readonly UserService _service = userService;

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] CreateAccountDTO user) => _service.AddUser(user) ? Ok("sucessfully added") : BadRequest("error adding user");

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO login) => _service.Login(login) != null ? Ok() : BadRequest("error logging in");

        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassDTO newPass) => _service.ResetPassword(newPass) != null ? Ok("password reset") : BadRequest("error resetting password");
    }
}