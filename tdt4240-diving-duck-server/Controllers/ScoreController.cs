using DivingDuckServer.Data;
using DivingDuckServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static DivingDuckServer.Controllers.UserController;

namespace DivingDuckServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly MyContext _context;

        public ScoreController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Score
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScoreResponse>>> GetScores()
        {
            if (_context.Scores == null)
            {
                return NotFound();
            }
            return await _context.Scores.Include(s => s.User).Select(s => new ScoreResponse
            {
                Id = s.Id,
                TimeElapsed = s.TimeElapsed
            }).ToListAsync();
        }

        // GET: api/Score/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            if (_context.Scores == null)
            {
                return NotFound();
            }
            var score = await _context.Scores.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        // PUT: api/Score/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
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

        // POST: api/Score
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserController.ScoreResponse>> PostScore(ScoreDTO score)
        {
            if (_context.Scores == null)
            {
                return Problem("Entity set 'ScoreContext.Scores'  is null.");
            }
            var user = _context.Users.FirstOrDefault(c => c.Id == score.UserId);
            if (user == null)
            {
                return Problem("User does not exist");
            }
            var scoreItem = new Score { TimeElapsed = score.TimeElapsed, User = user };

            _context.Scores.Add(scoreItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostScore", new UserController.ScoreResponse { Id = scoreItem.Id, TimeElapsed = scoreItem.TimeElapsed });
        }

        // DELETE: api/Score/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            if (_context.Scores == null)
            {
                return NotFound();
            }
            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScoreExists(int id)
        {
            return (_context.Scores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
