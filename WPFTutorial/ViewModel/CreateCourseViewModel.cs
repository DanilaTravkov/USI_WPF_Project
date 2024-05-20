using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class CreateCourseViewModel
    {
        public ELevel CourseLevel;
        public int? WeeksDuration;
        public List<WeekDays> WeekDays;
        public DateOnly? StartsAt;
        public bool? IsOnline;
        public int? MaxStudents;
        public DateTime ClassDuration; // readonly
        public string? CourseName;
        public Teacher Teacher;

        public ICommand CreateCourseCommand { get; set; }

        public CreateCourseViewModel()
        {
            CreateCourseCommand = new RelayCommand(CreateCourse, CanCreateCourse);
        }

        private bool CanCreateCourse(object obj)
        {
            return UserSession.Instance.IsTeacher();
        }

        private void CreateCourse(object obj)
        {
            if (string.IsNullOrEmpty(CourseName) || WeeksDuration <= 0 || WeeksDuration == null || StartsAt == null || MaxStudents <= 0)
            {
                    MessageBox.Show("Some input(s) are empty!");
            }
            if (Teacher == null)
            {
                MessageBox.Show("You must be logged in as a teacher to create a course.");
                return;
            }
            else
            {
                Teacher = UserSession.Instance.LoggedInUser as Teacher;
                Course newCourse = new Course 
                {
                    Teacher = Teacher,
                    CourseLevel = CourseLevel,
                    WeeksDuration = WeeksDuration,
                    StartsAt = StartsAt,
                    IsOnline = IsOnline,
                    MaxStudents = MaxStudents,
                    CourseName = CourseName
                };

                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Courses.Add(newCourse);
                    dbContext.SaveChanges();
                    MessageBox.Show("Succsessfully created a new course");
                }
            }
        }
    }
}
