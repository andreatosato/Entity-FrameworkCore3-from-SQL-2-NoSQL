using System;
using System.Collections.Generic;
using System.Linq;

namespace EF3.NoSqlEntityModels
{
	public class Exam
	{
		public Exam(string code, ExamType examType, DateTimeOffset examDate)
		{
			Code = code;
			ExamType = examType;
			ExamDate = examDate;
		}

		public string Code { get;  }
		public ExamType ExamType { get; }
		public DateTimeOffset ExamDate { get; }
		public string Classroom { get; set; }

		public HashSet<Student> Students { get; private set; } = new HashSet<Student>();
		public void AddStudent(Student student)
		{
			if(!Students.Any(x => x.IdentificationNumber == student.IdentificationNumber))
				Students.Add(student);
		}
	}

	public enum ExamType
	{
		PreExamSession,
		ExamSession,
		FirstHalf,
		SecondHalf,
		OralExam
	}
}
