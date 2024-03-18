using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckinAPI.Models;

namespace CheckinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly MarkContext _context;

        public MarkController(MarkContext context)
        {
            _context = context;
        }

        // GET: api/Mark
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mark>>> GetMark()
        {
            return await _context.Mark.ToListAsync();
        }

        // POST: api/Mark
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mark>> PostMarkItem(Mark mark)
        {
            if (isMarkedToday(mark.qq)) // You have marked today
                return NoContent();
            if (!isRegistered(mark.qq)) // You have to register before using mark function 
                return Unauthorized();
            _context.Mark.Add(mark);
            _context.Marklog.Add(new Marklog(mark.qq, DateTime.Now.ToString(), mark.pic_url, mark.word_num));
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetMark), new { id = mark.id }, mark);
        }

        private bool isMarkedToday(string qq)
        {
            foreach(var elem in _context.Marklog.Where(e => e.qq == qq).ToList())
                if (elem.date != null && elem.date[..10] == DateTime.Now.ToString()[..10])
                    return true;
            return false;
        }
        private bool isRegistered(string qq)
        {
            return _context.Register.Any(e => e.qq == qq);
        }
    }
}
