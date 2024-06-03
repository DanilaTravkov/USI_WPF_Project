using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class StudentGradeTeacherViewModel
    {

        public int Grade { get; set; }

        public ICommand GradeTeacherCommand { get; set; }

        public StudentGradeTeacherViewModel()
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
                    
                    // TODO: Becomes empty every time the application restarts, fix
                    if (student.GradedTeacherIds.Contains(teacher.Id))
                    {
                        MessageBox.Show("You have already graded this teacher.");
                        return;
                    }
                    
                    // TODO: Never executes, fix
                    if (finishedCourse.StudentsCount == finishedCourse.MaxStudents)
                    {
                        MessageBox.Show("You are the last to grade");
                        teacher.SumTeacherGrade += Grade;
                        teacher.SumTeacherGrade = (float)(teacher.SumTeacherGrade / finishedCourse.MaxStudents);
                        MessageBox.Show("You graded your teacher");
                    }
                    else
                    {
                        MessageBox.Show("You graded your teacher");
                        teacher.SumTeacherGrade += Grade;
                    }
                    student.GradedTeacherIds.Add(teacher.Id);

                    dbContext.SaveChanges();
                }
            }
        }

    }
}
