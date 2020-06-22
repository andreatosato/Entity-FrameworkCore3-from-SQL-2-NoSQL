using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF3.SQLContext.Configurations
{
    public class TakenExamConfiguration : IEntityTypeConfiguration<TakenExam>
    {
        public void Configure(EntityTypeBuilder<TakenExam> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Exam).WithMany(x => x.TakenExams).IsRequired();
            builder.HasOne(x => x.Student).WithMany(t => t.TakenExams).IsRequired();
        }
    }
}
