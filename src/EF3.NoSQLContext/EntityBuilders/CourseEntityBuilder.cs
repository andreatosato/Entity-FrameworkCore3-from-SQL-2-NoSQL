using EF3.NoSqlEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace EF3.NoSQLContext.EntityBuilders
{
	public class CourseEntityBuilder : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd();
			builder.ToContainer("Courrrrse")
				.HasNoDiscriminator();


			builder.Property(x => x.Name);
			builder.Property(x => x.Teacher);
			builder.Property(x => x.CreditsNumber);

			JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
			{
				IgnoreNullValues = true,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = false
			};
			var converter = new ValueConverter<ExtraCredit, string>(
				v => JsonSerializer.Serialize(v, serializerOptions),
				v => JsonSerializer.Deserialize<ExtraCredit>(v, serializerOptions));
			builder.Property(x => x.ExtraCredits)
				   .HasConversion(converter);


			builder.HasQueryFilter(t => t.IsExpired == false);
			builder.OwnsMany<Exam>(e => e.Exams, t =>
			{
				t.Property(e => e.Code);
				// https://docs.microsoft.com/it-it/ef/core/modeling/value-conversions
				t.Property(e => e.ExamType)				
				 .HasConversion<string>();
				//.HasConversion(new EnumToStringConverter<ExamType>());
				t.Property(e => e.ExamDate);

				t.OwnsMany<Student>(s => s.Students, f =>
				{
					f.Property(s => s.IdentificationNumber);
					f.Property(e => e.CreateDate).HasField("_createDate");
					// https://docs.microsoft.com/it-it/ef/core/modeling/backing-field?tabs=data-annotations
					// .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);
					f.Property(e => e.UpdateDate).HasField("_updateDate");
					f.WithOwner().HasPrincipalKey(c => c.Code);

					// https://docs.microsoft.com/it-it/ef/core/modeling/owned-entities#implicit-keys
					f.OwnsOne(a => a.Address,
					a =>
					{
						a.ToJsonProperty("Indirizzi");
						a.Property(c => c.Street).ToJsonProperty("Via");
						a.Property(c => c.ZipCode);

						// WARNING
						a.WithOwner().HasPrincipalKey(r => r.IdentificationNumber);
					});
				});
			});
			
		}
	}
}
