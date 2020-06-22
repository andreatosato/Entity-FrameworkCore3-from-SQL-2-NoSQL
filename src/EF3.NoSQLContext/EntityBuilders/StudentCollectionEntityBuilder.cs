using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF3.NoSQLContext.EntityBuilders
{
	class StudentCollectionEntityBuilder : IEntityTypeConfiguration<StudentCollection>
	{
		public void Configure(EntityTypeBuilder<StudentCollection> builder)
		{
			builder.ToContainer("Student");
			builder.HasKey(x => x.IdentificationNumber);
			builder.Property(p => p.IdentificationNumber);
			builder.HasPartitionKey(x => x.Surname);

			builder.OwnsOne(a => a.Address,
				a =>
				{
					a.Property(c => c.Street);
					a.Property(c => c.Cap);
				}
			);
		}
	}
}
