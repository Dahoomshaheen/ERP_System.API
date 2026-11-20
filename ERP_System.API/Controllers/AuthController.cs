using ERP_System.Application.Interfaces;
using ERP_System.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IGenericRepository<User> _userRepo;

        public AuthController(IConfiguration config, IGenericRepository<User> userRepo)
        {
            _config = config;
            _userRepo = userRepo;
        }

        // Only Admin can create new employee accounts (Cashier or Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var users = await _userRepo.GetAllAsync();
            if (users.Any(u => u.UserName.ToLower() == user.UserName.ToLower()))
            {
                return BadRequest(new { Message = $"Username '{user.UserName}' is already taken." });
            }

            // Normalize role (default to Cashier if empty or invalid)
            user.Role = (user.Role == "Admin") ? "Admin" : "Cashier";

            await _userRepo.AddAsync(user);
            return StatusCode(201, new { Message = $"User '{user.UserName}' registered successfully as {user.Role}." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginInfo)
        {
            var users = await _userRepo.GetAllAsync();
            var user = users.FirstOrDefault(u => u.UserName == loginInfo.UserName && u.Password == loginInfo.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Incorrect username or password." });
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role,
                UserName = user.UserName
            });
        }
    }
}