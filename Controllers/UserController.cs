using FormBackend.Model;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase{
        private readonly UserService _service;
        public UserController(UserService userService){
            _service = userService;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers(){
            try{
                return Ok(_service.GetUsers());
            }catch(Exception error){
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] UserModel user){
            try{
                return Ok(_service.AddUser(user));
            }
            catch(Exception error){
                return BadRequest(error.Message);
            }
        }
    }
}