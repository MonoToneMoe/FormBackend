using FormBackend.Model;
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

        [HttpGet]
        [Route("GetAllUsers")]
        public IEnumerable<UserModel> GetAllUsers(){
            return _service.GetUsers();
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