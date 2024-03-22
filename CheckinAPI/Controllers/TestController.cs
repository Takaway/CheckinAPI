using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace CheckinAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get() {
			return NoContent();
		}
	}
}
