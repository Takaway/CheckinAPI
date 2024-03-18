using System.Security.Policy;

namespace CheckinAPI.Models
{
	public class Remind
	{
		public long id { get; set; } = default;
		public string qq { get; set; } = null!;
		public string time { get; set; } = null!;
	}
}
