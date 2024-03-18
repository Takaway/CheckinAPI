using Microsoft.AspNetCore.Mvc;
using CheckinAPI.Models;
using NuGet.Versioning;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace CheckinAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodaylistController : ControllerBase
	{
		private readonly MarkContext _context;

		public TodaylistController(MarkContext context)
		{
			_context = context;
		}

		// GET: api/Todaylist/1
		// oneday before
		[HttpGet("{offset}")]
		public ActionResult<IEnumerable<Todaylist>> GetTodaylist(int offset = 0)
		{
			var Bufferlist = _context.Marklog.ToList();
			var todaylist = new List<Todaylist>();
			foreach (var elem in Bufferlist)
				if (elem.qq != null && withinRange(elem.date, offset))
					todaylist.Add(new Todaylist(elem.qq, _context.Register.Where(e => e.qq != null && e.qq == elem.qq).Single().name, elem.pic_url, elem.word_num, elem.date));
			return todaylist;
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