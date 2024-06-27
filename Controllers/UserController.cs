using FormBackend.Models;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers{
    [Route("[controller]")]
    public class UserController(UserService data) : ControllerBase{
        private readonly UserService _data = data;

        [HttpPost]
        [Route("/AddUser")]
        public IActionResult AddUser(UserModel NewUser){
            try{
                _data.AddUser(NewUser);
                return Ok("successfully added");
            }
            catch(Exception error){
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [Route("/GetAllUers")]
        public IActionResult GetUsers(){
            try{
                var Users =_data.GetAllUsers();
                return Ok(Users);
            }catch(Exception error){
                return BadRequest(error.Message);
            }
        }
    }
}