using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF3.NoSqlEntityModels
{
	public class Course
	{
		public Course(string name, string teacher, int creditsNumber)
		{
			Name = name;
			Teacher = teacher;
			CreditsNumber = creditsNumber;
		}
		public Guid Id { get; }
		public string Name { get; }
		public string Teacher { get; }
		public int CreditsNumber { get; }
		public CourseType CourseType { get; set; }
		public HashSet<Exam> Exams { get; private set; } = new HashSet<Exam>();
		public void AddExam(Exam exam)
		{
			if (!Exams.Any(x => x.Code == exam.Code))
				Exams.Add(exam);
		}
		public ExtraCredit ExtraCredits { get; set; } = new ExtraCredit();
		public byte[] RowVersion { get; }
		public void SetExpired()
		{
			IsExpired = true;
		}
		public bool IsExpired { get; private set; }
	}

	public enum CourseType
	{
		Mandatory,
		Optional
	}

	public class ExtraCredit
	{
		public string Name { get; set; }
		public short Credits { get; set; }
		public int HoursSum { get; set; }
	}
}
