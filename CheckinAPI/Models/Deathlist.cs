namespace CheckinAPI.Models
{
	public class Deathlist
	{
		public Deathlist() { }
		public Deathlist(string qq, string name) { 
			this.qq = qq;
			this.name = name;
		}
		public string qq { get; set; } = null!;
		public string name { get; set; } = null!;
	}
}
