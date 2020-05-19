using EF3.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace EF3.NoSQLContext.EntityBuilders
{
	public class CourseEntityBuilder : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(p => p.Id).ValueGeneratedOnAdd();
			builder.ToContainer("Courrrrse")
				.HasNoDiscriminator();

			

			builder.Property(x => x.Name);
			builder.Property(x => x.Teacher);
			builder.Property(x => x.CreditsNumber);
			builder.OwnsOne<ExtraCredit>(e => e.ExtraCredits, t =>
			{
				t.Property(x => x.Name);
				t.Property(x => x.HoursSum);
				t.Property(x => x.Credits);
			});
			builder.OwnsMany<Exam>(e => e.Exams, t =>
			{
				t.Property(e => e.Code);
				t.Property(e => e.ExamType);
				t.Property(e => e.ExamDate);

				t.OwnsMany<Student>(s => s.Students, f =>
				{
					f.Property(s => s.Freshman);

					f.OwnsOne(a => a.Address,
					a =>
					{
						a.ToJsonProperty("Indirizzi");
						a.Property(c => c.Street);
						a.Property(c => c.City);
						a.Property(c => c.Cap);
					});
				});
			});
			
		}
	}
}
