using EF3.SQLContext;
using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF3.SQLApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var context = new DataContext();
            await context.Database.MigrateAsync();

            await InitializeAsync(context);

            var
        }

        private static async Task InitializeAsync(DataContext context)
        {
            if (await context.Students.AnyAsync())
            {
                return;
            }

            var marioRossi = new Student
            {
                FirstName = "Mario",
                LastName = "Rossi",
                IdentificationNumber = "2020-1234",
                Email = "mario.rossi@unitest.it",
                Address = new Address("Via Verdi, 24", "20121", "Milano")
            };

            var marcoMinerva = new Student
            {
                FirstName = "Marco",
                LastName = "Minerva",
                IdentificationNumber = "232440",
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

            var architetturaDegliElaboratori = new Course
            {
                Name = "Architettura degli Elaboratori",
                Teacher = "Paolino Paperino",
                Credits = 9,
                Type = CourseType.Mandatory
            };

            var costruzioneDiInterfacce = new Course
            {
                Name = "Costruzione di Interfacce",
                Teacher = "Paperoga",
                Credits = 6,
                Type = CourseType.Optional
            };

            var analisiMatematica = new Course
            {
                Name = "Analisi Matematica",
                Teacher = "Pietro Gambadilegno",
                Credits = 6,
                Type = CourseType.Mandatory
            };

            context.Courses.Add(architetturaDegliElaboratori);
            context.Courses.Add(costruzioneDiInterfacce);
            context.Courses.Add(analisiMatematica);

            var costruzioneDiInterfacceMidTermExam = new Exam
            {
                Classroom = "Aula A",
                Code = "CI-001",
                Date = new DateTime(2020, 4, 14, 9, 30, 0),
                Type = ExamType.MidTerm,
                Course = costruzioneDiInterfacce
            };

            var costruzioneDiInterfacceFinalExam = new Exam
            {
                Classroom = "Aula A",
                Code = "CI-002",
                Date = new DateTime(2020, 6, 30, 14, 0, 0),
                Type = ExamType.Final,
                Course = costruzioneDiInterfacce
            };

            var analisiMatematicExam = new Exam
            {
                Classroom = "Aula D",
                Code = "AM",
                Date = new DateTime(2020, 6, 29, 11, 0, 0),
                Type = ExamType.Session,
                Course = analisiMatematica
            };

            context.Exams.Add(costruzioneDiInterfacceMidTermExam);
            context.Exams.Add(costruzioneDiInterfacceFinalExam);
            context.Exams.Add(analisiMatematicExam);

            var marcoMinervaCostruzioneDiInterfacciaMidTerm = new TakenExam
            {
                Exam = costruzioneDiInterfacceMidTermExam,
                Student = marcoMinerva,
                Score = 30
            };

            var marcoMinervaCostruzioneDiInterfacciaFinal = new TakenExam
            {
                Exam = costruzioneDiInterfacceFinalExam,
                Student = marcoMinerva,
                Score = 30
            };

            var marcoMinervaAnalisiMatematica = new TakenExam
            {
                Exam = analisiMatematicExam,
                Student = marcoMinerva,
                Score = 26
            };

            var marioRossiAnalisiMatematica = new TakenExam
            {
                Exam = analisiMatematicExam,
                Student = marioRossi,
                Score = 24
            };

            context.TakenExams.Add(marcoMinervaCostruzioneDiInterfacciaMidTerm);
            context.TakenExams.Add(marcoMinervaCostruzioneDiInterfacciaFinal);
            context.TakenExams.Add(marcoMinervaAnalisiMatematica);
            context.TakenExams.Add(marioRossiAnalisiMatematica);

            await context.SaveChangesAsync();
        }
    }
}
