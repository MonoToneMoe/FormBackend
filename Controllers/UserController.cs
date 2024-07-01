using FormBackend.Model.DTOS;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;
namespace FormBackend.Controllers{
    [ApiController] [Route("[controller]")]
    public class UserController(UserService userService) : ControllerBase{
        private readonly UserService _service = userService;
        [HttpPost] [Route("AddUser")] public IActionResult AddUser([FromBody] CreateAccountDTO user) => _service.AddUser(user) ? Ok("sucessfully added") : BadRequest("error adding user");
        [HttpPost] [Route("Login")] public string Login([FromBody] LoginDTO user) => _service.Login(user) ? "Success" : "Failed";
        [HttpGet] [Route("GetAllUsers")] public IEnumerable<UserDTO> GetAllUsers() => _service.GetUsers();
        [HttpPut] [Route("ResetPassword")] public IActionResult ResetPassword([FromBody] ResetPassDTO newPass) => _service.ResetPassword(newPass) ? Ok("Password reset") : BadRequest("Error resetting password");
        [HttpPut] [Route("EditUser")] public IActionResult EditUser([FromBody] CreateAccountDTO UserToUpdate) => _service.EditUser(UserToUpdate) ? Ok("Successfully Updated") : BadRequest("Error updating user");
        [HttpDelete] [Route("DeleteUser/{id}")] public IActionResult DeleteUser(int id) => _service.DeleteUser(id) ? Ok("Successfully Deleted") : BadRequest("Error deleting user");
    }
}