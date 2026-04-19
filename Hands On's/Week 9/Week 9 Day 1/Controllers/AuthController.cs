using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week9_Day1_ContactManagementApi.Data;
using Week9_Day1_ContactManagementApi.DTOs;
using Week9_Day1_ContactManagementApi.Models;
using Week9_Day1_ContactManagementApi.Services;

namespace Week9_Day1_ContactManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly PasswordHasher<UserInfo> _passwordHasher = new();

        public AuthController(AppDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allowedRoles = new[] { "Admin", "User" };
            if (!allowedRoles.Contains(dto.Role))
                return BadRequest("Role must be Admin or User.");

            var existing = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == dto.EmailId);
            if (existing != null)
                return BadRequest("User already exists.");

            var user = new UserInfo
            {
                EmailId = dto.EmailId,
                Role = dto.Role
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == dto.EmailId);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var verify = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (verify == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid email or password.");

            return Ok(_jwtTokenService.GenerateToken(user));
        }
    }
}
