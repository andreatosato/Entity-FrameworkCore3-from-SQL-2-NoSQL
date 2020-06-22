using EF3.SQLContext.Extensions;
using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF3.SQLContext.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IdentificationNumber).HasMaxLength(10).IsRequired();
            builder.HasIndex(x => x.IdentificationNumber).IsUnique();

            builder.Property(x => x.FirstName).HasMaxLength(80).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(80).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(80);

            builder.OwnsOne(x => x.Address, od =>
             {
                 od.Property(x => x.Street).HasMaxLength(100);
                 od.Property(x => x.ZipCode).HasMaxLength(5);
                 od.Property(x => x.City).HasMaxLength(50);
             });

            builder.HasMany(x => x.TakenExams);
            builder.Property(x => x.ExtraCredits).HasJsonConversion(required: false);
        }
    }
}
