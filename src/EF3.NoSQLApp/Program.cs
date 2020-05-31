using EF3.EntityModels;
using EF3.NoSQLContext;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using Newtonsoft.Json.Linq;
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
					// https://github.com/dotnet/efcore/blob/master/src/EFCore.Cosmos/Storage/Internal/CosmosExecutionStrategy.cs
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

				#region [Student2]
				var giuseppeVerdi = new Student("2020-567");
				giuseppeVerdi.Name = "Giuseppe";
				giuseppeVerdi.Surname = "Verdi";
				giuseppeVerdi.SetMail("giuseppe.verdi@unitest.it");
				giuseppeVerdi.Address = new Address("Via Rossi, 44", 20121) { City = "Piacenza" };
				#endregion

				#region [Exam]
				var exam = new Exam("Analisi1-2020-Completo", ExamType.ExamSession, new DateTimeOffset(2020, 06, 01, 09, 00, 00, TimeSpan.Zero));
				exam.Classroom = "Aula 2B";
				exam.AddStudent(marioRossi);
				exam.AddStudent(giuseppeVerdi);
				#endregion

				#region [Course]
				var teacher = "Isaac Newton";
				var analisi1 = new Course("Analisi 1", teacher, 12);
				analisi1.AddExam(exam);
				analisi1.ExtraCredits = new ExtraCredit
				{
					Credits = 10,
					HoursSum = 150,
					Name = "Workshop fisica"
				};
				#endregion


				#region [ExamDeleted]
				var examDeleted = new Exam("Fisica1-2020-Completo", ExamType.ExamSession, new DateTimeOffset(2020, 06, 01, 09, 00, 00, TimeSpan.Zero));
				examDeleted.Classroom = "Aula 1B";
				examDeleted.AddStudent(giuseppeVerdi);
				#endregion

				#region [CourseToDeleted]
				var teacherDeleted = "Archimede";
				var fisica1 = new Course("Fisica 1", teacherDeleted, 12);
				fisica1.AddExam(examDeleted);
				fisica1.ExtraCredits = new ExtraCredit
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
				db.Courses.Add(fisica1);
				await db.SaveChangesAsync();

				// Print
				await PrintCourse(db.Courses);

				var course = await db.Courses.FindAsync(fisica1.Id);
				course.SetExpired();
				await db.SaveChangesAsync();

				await PrintCourse(db.Courses);


				await CosmosClientCourseAsync(db, course);
				await CosmosClientStudentCollectionAsync(db);
				Console.Read();
			}
		}

		private static async Task PrintCourse(DbSet<Course> courses)
		{
			var coursesList = await courses.ToListAsync();
			for (int i = 0; i < coursesList.Count(); i++)
			{
				var c = coursesList.ElementAt(i);
				Console.WriteLine($"Item Number: {i + 1}");
				Console.WriteLine($"Name: {c.Name}");
				Console.WriteLine($"Id: {c.Id}");
				Console.WriteLine($"CourseType: {c.CourseType}");
				Console.WriteLine($"IsExpired: {c.IsExpired}");
				Console.WriteLine("_________________________________");
			}
		}

		private static async Task CosmosClientCourseAsync(SampleContext db, Course course)
		{
			var cosmosClient = db.Database.GetCosmosClient();
			{
				var database = cosmosClient.GetDatabase("sample-db");
				var container = database.GetContainer("Courrrrse");

				var resultSet = container.GetItemQueryIterator<JObject>(new QueryDefinition("select * from o"));
				while (resultSet.HasMoreResults)
				{
					var coursesObject = await resultSet.ReadNextAsync();
					foreach (var item in coursesObject.AsJEnumerable())
					{
						Console.WriteLine($"Next Course: {Environment.NewLine} {item}");
						Console.WriteLine($"_____________________________________________");
					}
				}

				var courseFromCosmos = await container.ReadItemAsync<dynamic>(course.Id.ToString(), PartitionKey.None);
				Console.WriteLine($"Read From Cosmos Read Item Async: {Environment.NewLine} {courseFromCosmos.Resource}");
				Console.WriteLine($"_____________________________________________");
			}
		}

		private static async Task CosmosClientStudentCollectionAsync(SampleContext db)
		{
			var cosmosClient = db.Database.GetCosmosClient();
			{
				var database = cosmosClient.GetDatabase("sample-db");
				var container = database.GetContainer("Student");

				var resultSet = container.GetItemQueryIterator<JObject>(new QueryDefinition("select * from student"));
				while (resultSet.HasMoreResults)
				{
					var coursesObject = await resultSet.ReadNextAsync();
					foreach (var item in coursesObject.AsJEnumerable())
					{
						Console.WriteLine($"Next Student: {Environment.NewLine} {item}");
						Console.WriteLine($"_____________________________________________");
					}
				}

				var courseFromCosmos = await container.ReadItemAsync<StudentCollection>("StudentCollection|2020-1234", new PartitionKey("Rossi"));
				Console.WriteLine($"Read From Cosmos Read Item Async: {Environment.NewLine} {System.Text.Json.JsonSerializer.Serialize(courseFromCosmos.Resource, new System.Text.Json.JsonSerializerOptions { WriteIndented = true })}");
				Console.WriteLine($"_____________________________________________");
			}
		}
	}
}
