using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seylan_RED_BackEND.Models;

namespace Seylan_RED_BackEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly DBContext _context;

        public UserDetailsController(DBContext context)
        {
            _context = context;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDetails user)
        {
            if (_context.SeylanREDUsers.Any(u => u.UserName == user.UserName))
            {
                return Conflict("User already exists.");
            }

            await _context.SeylanREDUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // Login a user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDetails user)
        {
            var existingUser = await _context.SeylanREDUsers
                .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.Password == user.Password);

            if (existingUser == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("Login successful!");
        }

        // Get a specific user (helper method for Register)
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUser(int id)
        {
            var user = await _context.SeylanREDUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
