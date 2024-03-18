using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckinAPI.Models;

namespace CheckinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MarkContext _context;
        public RegisterController(MarkContext context)
        {
            _context = context;
        }

        // GET: api/Register
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Register>>> GetRegister()
        {
            return await _context.Register.ToListAsync();
        }

        // POST: api/Register
        // Use this to register
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Register>> PostRegister(Register register)
        {
            if (RegisterExists(register.qq))
                return NoContent();
            _context.Register.Add(register);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRegister), new { id = register.id }, register);
        }

        // PUT: api/Register/114514?name=ajisai
		[HttpPut("{qq}")]
		public async Task<IActionResult> PutMarkItem(string qq, string name)
		{
			if (!RegisterExists(qq))
				return NotFound();
            Models.Register register = _context.Register.Single(e => e.qq == qq);
            register.name = name;
			_context.Entry(register).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}

		//DELETE: api/Register/114514
		[HttpDelete("{qq}")]
		public async Task<IActionResult> DeleteRegister(string qq)
		{
			var register = await _context.Register.Where(e => e.qq == qq).ToListAsync();
			if (register.Count == 0)
			{
				return NotFound();
			}
			_context.Register.Remove(register.Single());
			await _context.SaveChangesAsync();
			return NoContent();
		}

		private bool RegisterExists(string qq)
        {
            return _context.Register.Any(e => e.qq == qq);
        }
    }
}
