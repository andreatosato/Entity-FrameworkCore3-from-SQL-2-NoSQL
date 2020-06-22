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

		public HashSet<StudentVote> Students { get; private set; } = new HashSet<StudentVote>();
		public void AddStudent(Student student)
		{
			if(!Students.Any(x => x.Student.IdentificationNumber == student.IdentificationNumber))
				Students.Add(new StudentVote { Student = student });
		}

		public void SetVote(Student student, int vote, DateTime voteTime)
		{
			var studentCurrent = Students.FirstOrDefault(x => x.Student.Equals(student));
			if(studentCurrent != null)
			{
				studentCurrent.VoteNumber = vote;
				studentCurrent.VoteDate = voteTime;
			}
		}
	}

	public class StudentVote
	{
		// WARNING: EF ChangeTrack
		public string StudentVoteId { get; set; } = Guid.NewGuid().ToString("N");
		public Student Student { get; set; }
		public DateTime? VoteDate { get; set; }
		public int? VoteNumber { get; set; }
	}

	public enum ExamType
	{
		Session,
		MidTerm,
		Final,
		Oral
	}
}
