using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase{
        private readonly UserService _service;
        public UserController(UserService userService){
            _service = userService;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] CreateAccountDTO user){
            try{
                return Ok(_service.AddUser(user));
            }
            catch(Exception error){
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO login){
            try{
                return Ok(_service.Login(login));
            }catch(Exception error){
                return BadRequest(error.Message);
            }
        }

        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassDTO newPass){
            try{
                return Ok(_service.ResetPassword(newPass));
            }
            catch(Exception error){
                return BadRequest(error.Message);
            }
        }
    }
}