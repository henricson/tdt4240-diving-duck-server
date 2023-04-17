using DivingDuckServer.Data;
using DivingDuckServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DivingDuckServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyContext _context;

        public UserController(MyContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Include(u => u.Scores).Select(u => new UserResponse { Id = u.Id, Scores = u.Scores.Select(s => new ScoreResponse { Id = s.Id, TimeElapsed = s.TimeElapsed }), UserName = u.UserName }).ToListAsync();
        }

        public class ScoreResponse
        {
            public int Id { get; set; }
            public float TimeElapsed { get; set; }
        }

        public class UserResponse
        {
            public int Id { get; set; }
            public string? UserName { get; set; }
            public IEnumerable<ScoreResponse> Scores { get; set; } = Array.Empty<ScoreResponse>();

        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Where(u => u.Id == id).Select(u => new UserResponse
            {
                Id = u.Id,
                UserName = u.UserName,
                Scores = u.Scores.Select(s => new ScoreResponse
                {
                    Id = s.Id,
                    TimeElapsed = s.TimeElapsed
                })
            }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ScoreContext.Users'  is null.");
            }
            User userItem = new User { UserName = user.UserName };

            _context.Users.Add(userItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = userItem.Id }, userItem);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
