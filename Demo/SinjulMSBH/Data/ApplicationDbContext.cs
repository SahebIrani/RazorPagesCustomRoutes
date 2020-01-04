using Demo.SinjulMSBH.Entities;

using Microsoft.EntityFrameworkCore;

namespace Demo.SinjulMSBH.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Person> People { get; set; }
	}
}
