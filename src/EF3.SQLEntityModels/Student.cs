using System;
using System.Collections.Generic;

namespace EF3.SQLEntityModels
{
    public class Student
    {
        public Guid Id { get; set; }

        public string IdentificationNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public DateTimeOffset CreateDate { get; set; }

        public ICollection<TakenExam> TakenExams { get; set; }

        public List<ExtraCredit> ExtraCredits { get; set; }

        public Student(string identificationNumber)
        {
            IdentificationNumber = identificationNumber;
            CreateDate = DateTime.UtcNow;
        }
    }
}
