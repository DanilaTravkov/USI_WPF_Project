using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class StudentSeeCoursesDataViewModel
    {
        public ObservableCollection<Course> StudentCourse { get; }

        public ICommand ShowGradeTeacherCommand { get; set; }

        public StudentSeeCoursesDataViewModel()
        {
            ShowGradeTeacherCommand = new RelayCommand(ShowGradeTeacher, CanShowGradeTeacher);

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

        private bool CanShowGradeTeacher(object obj)
        {
            return true;
        }

        private void ShowGradeTeacher(object obj)
        {
            var studentGradeTeacher = new WPFTutorial.View.StudentGradeTeacher();
            studentGradeTeacher.Show();
        }
    }
}
