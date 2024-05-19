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
        MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY
    }
    public class Course
    {
        public Course()
        {
            ClassDuration = new TimeOnly(1, 30);
        }
        [Key]
        public Guid? Id { get; set; }
        public ELevel CourseLevel {  get; set; }
        public int? WeeksDuration {  get; set; }
        public DateOnly? StartsAt {  get; set; }
        public bool? IsOnline {  get; set; }
        public int? MaxStudents { get; set; } = 0; // Add max students which can attend this course IF IsOnline is false
        public TimeOnly ClassDuration { get; } = new TimeOnly(1, 30); // 1 hour and 30 minutes (90 minutes)

        public string? CourseName {  get; set; }
    }
}
