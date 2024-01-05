using Microsoft.AspNetCore.Mvc;
using proeduedge.Models;
using proeduedge.Repository;
using proeduedge.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly FileService _fileService;

        public UserController(IUserRepository userRepository, FileService fileService)
        {
            _userRepository = userRepository;
            _fileService = fileService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Users>> CreateUser([FromBody] Users user)
        {
            var newUser = await _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("update-profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _userRepository.GetUser(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            await _userRepository.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            try
            {
                var result = await _fileService.UploadAsync("avatar", file);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "An error occurred while uploading the file.");
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login([FromBody] LoginModel login)
        {
            var user = await _userRepository.UserLogin(login);
            if (user == null)
            {
                return NotFound("User not found. Please check your email and password.");
            }
            return Ok(user);
        }   
    }
}
