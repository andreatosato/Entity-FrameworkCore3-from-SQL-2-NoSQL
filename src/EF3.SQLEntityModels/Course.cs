using System;
using System.Collections.Generic;

namespace EF3.SQLEntityModels
{
    public class Course
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Teacher { get; set; }

        public int Credits { get; set; }

        public CourseType Type { get; set; }

        public ICollection<Exam> Exams { get; set; }

        public bool IsExpired { get; set; }

        public Course(string name, string teacher, int credits)
        {
            Name = name;
            Teacher = teacher;
            Credits = credits;
        }
    }

    public enum CourseType
    {
        Mandatory,
        Optional
    }
}
