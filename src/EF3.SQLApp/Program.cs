using EF3.SQLContext;
using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF3.SQLApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            //optionsBuilder.UseSqlServer(DataContext.ConnectionString, options =>
            //{
            //    options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null);
            //});

            using var context = new DataContext();
            await context.Database.MigrateAsync();

            var marioRossi = new Student("2020-1234")
            {
                FirstName = "Mario",
                LastName = "Rossi",
                Email = "mario.rossi@unitest.it",
                Address = new Address("Via Verdi, 24", "20121", "Milano")
            };

            var marcoMinerva = new Student("232440")
            {
                FirstName = "Marco",
                LastName = "Minerva",
                Email = "mminerva@di.unipi.it",
                Address = new Address("Via Non Te Lo Dico, 1", "18018", "Taggia"),
                ExtraCredits = new List<ExtraCredit>
                {
                    new ExtraCredit { Name = "CTO for Startup", Credits = 3, Hours = 8 },
                    new ExtraCredit { Name = "Sicurezza informatica for dummies", Credits = 1, Hours = 2 }
                }
            };

            context.Students.Add(marioRossi);
            context.Students.Add(marcoMinerva);

            await context.SaveChangesAsync();
        }
    }
}
