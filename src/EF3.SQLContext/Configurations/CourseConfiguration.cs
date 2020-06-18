using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF3.SQLContext.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Teacher).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(10).IsRequired();

            builder.HasMany(x => x.Exams).WithOne(x => x.Course).IsRequired();
        }
    }
}
