using System.Collections;

namespace CheckinAPI.Models
{
	public class Marklog
	{
		public Marklog() { } 
		public Marklog(string qq, string date, string? pic_url, long word_num) {
			this.qq = qq;
			this.date = date;
			this.pic_url = pic_url;
			this.word_num = word_num;
		}
		public long id { get; set; } = default;
		public string qq { get; set; } = null!;
		public string? pic_url { get; set; }
		public long word_num { get; set; }
		public string date { get; set; } = null!;
	}
}
