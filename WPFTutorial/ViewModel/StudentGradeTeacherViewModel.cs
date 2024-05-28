using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class StudentGradeTeacherViewModel
    {

        public int Grade;

        public ICommand GradeTeacherCommand { get; set; }

        StudentGradeTeacherViewModel()
        {
            GradeTeacherCommand = new RelayCommand(GradeTeacher, CanGradeTeacher);
        }

        private bool CanGradeTeacher(object obj)
        {
            return true;
        }

        private void GradeTeacher(object obj)
        {
            if (UserSession.Instance.LoggedInUser is Student student)
            {
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    var finishedCourse = dbContext.Courses
                        .FirstOrDefault(c => c.Id == student.CourseId);

                    var teacher = dbContext.Teachers
                        .FirstOrDefault(t => t.Id == finishedCourse.TeacherId);

                    teacher.SumTeacherGrade += Grade;

                    // TODO: if the student is the last to grade the teacher then teacher.SumTeacherGrade / course.MaxStudents;
                }
            }
        }
    }
}
