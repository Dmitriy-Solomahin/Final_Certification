using ApiUsers.Abstraction;
using ApiUsers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ApiUsers.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("add_user")]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {
            var entityUser = _userRepository.AddUser(userDTO);

            if (entityUser != null)
            {
                var token = GenerateToken(entityUser);
                return Ok(token);
            }
            return NotFound("Пользователь с данным логином использует другой пароль)");
        }

        [HttpGet("get_users")]
        public IActionResult GetUsers() { 
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        
        [HttpDelete("delete_user")]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser([FromBody] UserName user)
        {
            var result = _userRepository.DeleteUser(user);
            return Ok(result);
        }


        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgoritms.HmacSha256);

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
