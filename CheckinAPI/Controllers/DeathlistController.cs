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
			DateTime t_current = DateTime.Now.AddDays(-offset);
			DateTime t_target = DateTime.Parse(date);
			(int, int, int) t1, t2;
			if (t_current.Hour < 5) // now is yesterday
				t_current = t_current.AddDays(-1);
			if (t_target.Hour < 5) // tar is yesterday
				t_target = t_target.AddDays(-1);
			t1 = (t_current.Year, t_current.Month, t_current.Day);
			t2 = (t_target.Year, t_target.Month, t_target.Day);
			if (t1 == t2)
				return true;
			return false;
		}
	}
}