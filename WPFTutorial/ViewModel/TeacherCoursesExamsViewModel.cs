using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    // Predavac 1, 3
    public class TeacherCoursesExamsViewModel
    {

        public ICommand? SeeCoursesCommand { get; set; }
        public ICommand? SeeExamsCommand { get; set; }

        public ICommand SortCoursesByCreationDateCommand { get; set; }
        public ICommand SortCoursesByLanguageCommand { get; set; }
        public ICommand SortCoursesByLevelCommand { get; set; }

        public ObservableCollection<Course>? TeacherCourses;

        public TeacherCoursesExamsViewModel() 
        {
            TeacherCourses = new ObservableCollection<Course>();
            SeeCoursesCommand = new RelayCommand(ListCoursesData, CanListCoursesData);
            // SeeExamsCommand = new RelayCommand();


            SortCoursesByCreationDateCommand = new RelayCommand(SortCoursesByCreationDate);
            SortCoursesByLanguageCommand = new RelayCommand(SortCoursesByLanguage);
            SortCoursesByLevelCommand = new RelayCommand(SortCoursesByLevel);
        }

        private bool CanListCoursesData(object obj)
        {
            return UserSession.Instance.IsTeacher(); // Only works if the logged in user is a teacher
        }

        private void ListCoursesData(object obj)
        {
            using (DatabaseContext dbContext = new DatabaseContext()) 
            {
                if (UserSession.Instance.LoggedInUser is Teacher teacher)
                {
                    var courses = dbContext.Courses.Where(c => c.Teacher.Id == teacher.Id).ToList();
                    TeacherCourses?.Clear();
                    foreach (var course in courses)
                    {
                        TeacherCourses?.Add(course);
                    }
                }
            }
        }

        private void SortCoursesByCreationDate(object obj)
        {
            var sortedCourses = TeacherCourses?.OrderBy(c => c.Id).ToList();
            TeacherCourses?.Clear();
            foreach (var course in sortedCourses)
            {
                TeacherCourses.Add(course);
            }
        }

        private void SortCoursesByLanguage(object obj)
        {
            var sortedCourses = TeacherCourses?.OrderBy(c => c.CourseName).ToList();
            TeacherCourses?.Clear();
            foreach (var course in sortedCourses)
            {
                TeacherCourses.Add(course);
            }
        }

        private void SortCoursesByLevel(object obj)
        {
            var sortedCourses = TeacherCourses?.OrderBy(c => c.CourseLevel).ToList();
            TeacherCourses?.Clear();
            foreach (var course in sortedCourses)
            {
                TeacherCourses.Add(course);
            }
        }
        // TODO: The teacher should also be able to list their exams and filter them
    }
}
