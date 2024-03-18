using Microsoft.EntityFrameworkCore;
using CheckinAPI.Models;

namespace CheckinAPI.Models
{
	public class MarkContext : DbContext
	{
		public MarkContext(DbContextOptions<MarkContext> options) : base(options)
		{
		}

		public DbSet<Mark> Mark { get; set; } = null!;
		public DbSet<Marklog> Marklog { get; set; } = null!;
	    public DbSet<Register> Register { get; set; } = null!;
		public DbSet<Remind> Remind { get; set; } = null!;
	}
}
