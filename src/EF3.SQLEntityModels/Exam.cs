using System;
using System.Collections.Generic;

namespace EF3.SQLEntityModels
{
    public class Exam
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public ExamType Type { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Classroom { get; set; }

        public Guid CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<TakenExam> TakenExams { get; set; }
    }

    public enum ExamType
    {
        Session,
        MidTerm,
        Final,
        Oral
    }
}
