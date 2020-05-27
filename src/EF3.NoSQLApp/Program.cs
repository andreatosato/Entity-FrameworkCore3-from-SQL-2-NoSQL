using EF3.EntityModels;
using EF3.NoSQLContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace EF3.NoSQLApp
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<SampleContext>();
			optionsBuilder.UseCosmos(
				accountEndpoint: "https://localhost:8081",
				accountKey: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
				databaseName: "sample-db",
				// https://docs.microsoft.com/it-it/ef/core/miscellaneous/connection-resiliency
				// https://docs.microsoft.com/it-it/dotnet/architecture/microservices/implement-resilient-applications/implement-resilient-entity-framework-core-sql-connections
				options =>
				{
					options.ExecutionStrategy(d => new CosmosExecutionStrategy(d));
				});
			optionsBuilder.EnableSensitiveDataLogging();
			using (SampleContext db = new SampleContext(optionsBuilder.Options))
			{
				await db.Database.EnsureDeletedAsync();
				await db.Database.EnsureCreatedAsync();

				#region [Student]
				var marioRossi = new Student("2020-1234");
				marioRossi.Name = "Mario";
				marioRossi.Surname = "Rossi";
				marioRossi.SetMail("mario.rossi@unitest.it");
				marioRossi.Address = new Address("Via Verdi, 24", 20121) { City = "Milano" };
				#endregion

				#region [Exam]
				var exam = new Exam("Analisi1-2020-Completo", ExamType.ExamSession, new DateTimeOffset(2020, 06, 01, 09, 00, 00, TimeSpan.Zero));
				exam.Classroom = "Aula 2B";
				exam.AddStudent(marioRossi);
				#endregion

				#region [Course]
				var teacher = "Mario Rossi";
				var analisi1 = new Course("Analisi 1", teacher, 12);
				analisi1.AddExam(exam);
				analisi1.ExtraCredits = new ExtraCredit
				{
					Credits = 10,
					HoursSum = 150,
					Name = "Workshop fisica"
				};
				#endregion

				#region [StudentCollection]
				var marioRossiCollection = new StudentCollection("2020-1234");
				marioRossiCollection.Name = "Mario";
				marioRossiCollection.Surname = "Rossi";
				marioRossiCollection.SetMail("mario.rossi@unitest.it");
				marioRossiCollection.Address = new AddressCollection("Via Verdi, 24", 20121) { City = "Milano" };
				db.Students.Add(marioRossiCollection);
				#endregion


				db.Courses.Add(analisi1);
				await db.SaveChangesAsync();

				// Print
				PrintCourse(db.Courses);

				var course = await db.Courses.FindAsync(analisi1.Id);
				course.SetExpired();
				await db.SaveChangesAsync();

				PrintCourse(db.Courses);

			}
		}

		private static void PrintCourse(DbSet<Course> courses)
		{
			foreach (var item in courses)
			{
				Console.WriteLine($"Name: {item.Name}");
				Console.WriteLine($"Id: {item.Id}");
				Console.WriteLine($"CourseType: {item.CourseType}");
				Console.WriteLine($"IsExpired: {item.IsExpired}");
			}
		}
	}
}
