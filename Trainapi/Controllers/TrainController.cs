using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainapi.Data;
using Trainapi.Models; 

namespace Trainapi.Controllers
{
    [Route("api/train")]
    [ApiController]
    [Authorize]
    public class TrainController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TrainController(AppDbContext context)
        {
            _context = context;
        }

        //all users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Trains.ToListAsync();
            return Ok(users);
        }

        // Create or Edit train records
        [HttpPost]
        public async Task<IActionResult> CreateEdit([FromBody] List<Train> booking)
        {
            if (booking == null || !booking.Any())
            {
                return BadRequest("Invalid train data.");
            }

            await _context.Trains.AddRangeAsync(booking);
            await _context.SaveChangesAsync();
            return Ok(booking);
        }

        // Get train by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Trains.FindAsync(id);
            if (result == null)
            {
                return NotFound("Train not found.");
            }
            return Ok(result);
        }

        // Delete train by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.Trains.FindAsync(id);
            if (result == null)
            {
                return NotFound("Train not found.");
            }

            _context.Trains.Remove(result);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}






