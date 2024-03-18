using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckinAPI.Models;

namespace CheckinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarklogController : ControllerBase
    {
        private readonly MarkContext _context;

        public MarklogController(MarkContext context)
        {
            _context = context;
        }

        // GET: api/Marklog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marklog>>> GetMarklog()
        {
            return await _context.Marklog.ToListAsync();
        }

        // GET: api/Marklog/114514
        [HttpGet("{qq}")]
        public async Task<ActionResult<IEnumerable<Marklog>>> GetMarklog(string qq)
        {
            var marklog = await _context.Marklog.Where(e => e.qq ==qq).ToListAsync();
            if (marklog.Count == 0)
                return NotFound();
            return marklog;
        }
    }
}
