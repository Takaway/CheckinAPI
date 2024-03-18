using Org.BouncyCastle.Bcpg.OpenPgp;

namespace CheckinAPI.Models
{
	public class Todaylist
	{
		public Todaylist() { }
		public Todaylist(string qq, string name, string? pic_url, long word_num, string time)
		{
			this.qq = qq;
			this.name = name;
			this.pic_url = pic_url;
			this.word_num = word_num;
			this.time = time;
		}
		public string qq { get; set; } = null!;
		public string name { get; set; } = null!;
		public string? pic_url { get; set; }
		public long word_num {  get; set; }
		public string time { get; set; } = null!;
	}
}
