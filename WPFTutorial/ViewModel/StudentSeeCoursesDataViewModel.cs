using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class StudentSeeCoursesDataViewModel
    {
        public ObservableCollection<Course>  StudentCourse { get; }

        public StudentSeeCoursesDataViewModel()
        {
            var currentStudent = UserSession.Instance.LoggedInUser as Student;
            StudentCourse = new ObservableCollection<Course>();
            using (var dbContext = new DatabaseContext())
            {
                var course = dbContext.Courses
                    .Include(c => c.Teacher)
                    .FirstOrDefault(c => c.Id == currentStudent.CourseId);
                if (course != null)
                {
                    StudentCourse.Add(course);
                }
            }
        }
    }
}
