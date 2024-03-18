using Microsoft.AspNetCore.Mvc;
using CheckinAPI.Models;
using NuGet.Versioning;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.Json;
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