using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string ExamName {  get; set; }
        public int MaxNumberOfStudents {  get; set; }
        public string Language {  get; set; }
        public ELevel LanguageLevel {  get; set; }
        public DateTime ExamDate {  get; set; }
        public TimeOnly Duration { get; } = new TimeOnly(0, 40);
        public bool CanBeEdited => DateTime.Now >= ExamDate.AddDays(-14);


    }
}
