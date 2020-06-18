using EF3.SQLContext.Configurations;
using EF3.SQLEntityModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace EF3.SQLContext
{
    public class DataContext : DbContext
    {
        public const string ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=University;Integrated Security=True";

        public DataContext()
        {
        }

        //public DataContext(DbContextOptions<DataContext> options)
        //    : base(options)
        //{
        //}

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<TakenExam> TakenExams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString, options =>
             {
                 options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null);
             });

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new ExamConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new TakenExamConfiguration());
        }
    }
}
