using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckinAPI.Models;
using System.Xml.Linq;

namespace CheckinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindController : ControllerBase
    {
        private readonly MarkContext _context;

        public RemindController(MarkContext context)
        {
            _context = context;
        }

        // GET: api/Remind
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Remind>>> GetRemind()
        {
            return await _context.Remind.ToListAsync();
        }

        // GET: api/Remind/114514
        [HttpGet("{qq}")]
        public async Task<ActionResult<Remind>> GetRemind(string qq)
        {
            var remind = await _context.Remind.Where(e => e.qq == qq).ToListAsync();
            if ( remind.Count == 0)
            {
                return NotFound();
            }
            return remind.Single();
        }

        // PUT: api/Remind/114514?time=23:14
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{qq}")]
        public async Task<IActionResult> PutRemind(string qq, string time)
        {
			if (!RemindExists(qq))
				return NotFound();
			Remind remind = _context.Remind.Single(e => e.qq == qq);
			remind.time = time;
			_context.Entry(remind).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}

        // POST: api/Remind
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Remind>> PostRemind(Remind remind)
        {
            if (!isRegistered(remind.qq))
                return Unauthorized();
            if (RemindExists(remind.qq))
                return NoContent();
            _context.Remind.Add(remind);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRemind), new { id = remind.id }, remind);
        }

        // DELETE: api/Remind/qq
        [HttpDelete("{qq}")]
        public async Task<IActionResult> DeleteRemind(string qq)
        {
            var remind = await _context.Remind.Where(e => e.qq == qq).ToListAsync();
            if (remind.Count == 0)
            {
                return NotFound();
            }
            _context.Remind.Remove(remind.Single());
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RemindExists(string qq)
        {
            return _context.Remind.Any(e => e.qq == qq);
        }
		private bool isRegistered(string qq)
		{
			return _context.Register.Any(e => e.qq == qq);
		}
	}
}
