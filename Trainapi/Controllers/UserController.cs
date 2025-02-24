using Trainapi.Data;
using Trainapi.DTO;
using Trainapi.Models;
using Trainapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Trainapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;
        public UserController(AppDbContext appDbContext)
        {
            _context = appDbContext;
            _jwtService = new JwtService("refdrewsaqxdcvfgtrhyjuinhgtfrdewsazxcvfgbhnjuioklpoimnhgtrfcdeswertgfv");
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            if (model == null)
            {
                return BadRequest("Invalid user details");
            }
            await _context.users.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok("user created successfully");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterModel model)
        {

            var user = _context.users.Where(e => e.Email.ToLower() == model.Email.ToLower() && e.Password == model.Password).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Invalid user details");
            }
            //generate JWT token

            return Ok(_jwtService.GenerateToken(user.Email));
        }
    }
}
