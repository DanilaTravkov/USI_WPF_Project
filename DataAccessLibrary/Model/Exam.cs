﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Model
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string ExamName { get; set; }
        public int MaxNumberOfStudents { get; set; }
        public string Language { get; set; }
        public ELevel LanguageLevel { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeOnly Duration { get; } = new TimeOnly(4, 0); // Changed to 4 hours
        public bool CanBeEdited => DateTime.Now <= ExamDate.AddDays(-14);

        // reference fields

        public ICollection<Student> Students { get; set; }
        public int CreatorId { get; set; }
        public string CreatorType { get; set; } // Since both director and teachers can create exams
    }
}
