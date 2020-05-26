using EF3.EntityModels;
using EF3.NoSQLContext.EntityBuilders;
using Microsoft.EntityFrameworkCore;

namespace EF3.NoSQLContext
{
	public class SampleContext : DbContext
	{		
		public SampleContext(DbContextOptions options)
			: base(options)
		{

		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<StudentCollection> Students { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CourseEntityBuilder());
			modelBuilder.ApplyConfiguration(new StudentCollectionEntityBuilder());
		}
	}
}
