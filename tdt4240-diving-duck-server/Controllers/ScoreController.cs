using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DivingDuckServer.Data;
using DivingDuckServer.Models;

namespace tdt4240_diving_duck_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreContext _context;

        public ScoreController(ScoreContext context)
        {
            _context = context;
        }

        // GET: api/Score
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
        {
          if (_context.Scores == null)
          {
              return NotFound();
          }
            return await _context.Scores.ToListAsync();
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
            if (id != score.ScoreId)
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
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
          if (_context.Scores == null)
          {
              return Problem("Entity set 'ScoreContext.Scores'  is null.");
          }
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScore", new { id = score.ScoreId }, score);
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
            return (_context.Scores?.Any(e => e.ScoreId == id)).GetValueOrDefault();
        }
    }
}
