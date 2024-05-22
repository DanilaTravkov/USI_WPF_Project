using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTutorial.Model
{
    // Notes: Schedule of teacher's courses cannot confilct with schedules of other courses and exams held by this teacher
    // When a Teacher is deleted all assosiated courses are also deleted, hint: CASCADE 
    public enum ELevel
    {
        A1, A2, B1, B2, C1, C2
    }
    public enum WeekDays
    {
        MONDAY = 1, TUESDAY = 2, WEDNESDAY = 3, THURSDAY = 4, FRIDAY = 5, SATURDAY = 6, SUNDAY = 7
    }
    public class Course
    {
        public Course()
        {
            ClassDuration = new TimeOnly(1, 30);
        }
        [Key]
        public int Id { get; set; }
        public ELevel CourseLevel {  get; set; }
        public int? WeeksDuration {  get; set; }
        public List<WeekDays> WeekDays { get; set; }
        public DateTime? StartsAt { get; set; }
        public bool? IsOnline {  get; set; }
        public int? MaxStudents { get; set; } = 0; // TODO: Add max students which can attend this course IF IsOnline is false
        public TimeOnly ClassDuration { get; } = new TimeOnly(1, 30); // 1 hour and 30 minutes (90 minutes)
        public string? CourseName {  get; set; }

        // relational fields

        public Teacher Teacher {  get; set; }
        public int TeacherId { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>(); // A course can have many students

    }
}
