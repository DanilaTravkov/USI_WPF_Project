using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string ExamName {  get; set; }
        public int MaxNumberOfStudents {  get; set; }
        public DateTime StartsOn {  get; set; }
        public TimeOnly Duration { get; } = new TimeOnly(0, 40);

        public bool CanBeEdited
        {
            get
            {
                DateTime limit = StartsOn;
                limit.AddDays(-7);
                return DateTime.Now >= limit;
            }
        }

    }
}
