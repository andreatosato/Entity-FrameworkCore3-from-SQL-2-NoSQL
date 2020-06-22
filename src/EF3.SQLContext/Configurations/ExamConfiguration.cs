using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF3.SQLContext.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Code).HasMaxLength(10).IsRequired();
            builder.HasIndex(x => x.Code).IsUnique();

            builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(10).IsRequired();
            builder.Property(x => x.Classroom).HasMaxLength(20).IsRequired();

            builder.HasMany(x => x.TakenExams);
        }
    }
}
