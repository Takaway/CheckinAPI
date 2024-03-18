using Microsoft.AspNetCore.Mvc;
using CheckinAPI.Models;
using NuGet.Versioning;

namespace CheckinAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeathlistController : ControllerBase
	{
		private readonly MarkContext _context;

		public DeathlistController(MarkContext context)
		{
			_context = context;
		}

		// GET: api/Deathlist
		[HttpGet("{offset}")]
		public ActionResult<IEnumerable<Deathlist>> GetDeathlist(int offset = 0)
		{
			var deathlist = new List<Deathlist>();
			var todaylist = new List<string>();
			foreach (var elem in _context.Marklog.ToList())
				if (elem.qq != null && withinRange(elem.date, offset))
					todaylist.Add(elem.qq);
			foreach (var elem in _context.Register.ToList())
				if (!todaylist.Contains(elem.qq))
					deathlist.Add(new Deathlist(elem.qq, elem.name));
			return deathlist;
		}
		private bool withinRange(string date, int offset)
		{
			DateTime t_cur = DateTime.Parse(date);
			DateTime t_target = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0) - new TimeSpan(24 * offset, 0, 0);
			if (t_cur >= t_target && t_cur <= t_target + new TimeSpan(24, 0, 0))
				return true;
			return false;
		}
	}
}