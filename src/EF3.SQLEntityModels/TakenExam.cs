﻿using System;

namespace EF3.SQLEntityModels
{
    public class TakenExam
    {
        public Guid Id { get; set; }

        public Guid ExamId { get; set; }

        public Exam Exam { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public int Score { get; set; }
    }
}
