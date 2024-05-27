using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;
using WPFTutorial.View;

namespace WPFTutorial.ViewModel
{
    public class TeacherCoursesExamsViewModel : INotifyPropertyChanged
    {
        private Course selectedCourse;
        public Course SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
                OnPropertyChanged();
            }
        }

        public ICommand SortCoursesByCreationDateCommand { get; set; }
        public ICommand SortCoursesByLanguageCommand { get; set; }
        public ICommand SortCoursesByLevelCommand { get; set; }
        public ICommand ShowCreateCourseCommand { get; set; }
        public ICommand ShowUpdateSelectedCourseCommand { get; set; }
        public ICommand ViewCourseApplicationsCommand { get; set; }

        public ICommand FinishCourseCommand {  get; set; }
        public ICommand ShowCreateExamCommand { get; set; }

        public ObservableCollection<Course> TeacherCourses { get; set; }

        public TeacherCoursesExamsViewModel()
        {
            TeacherCourses = new ObservableCollection<Course>();

            SortCoursesByCreationDateCommand = new RelayCommand(SortCoursesByCreationDate, CanSortCoursesByCreationDate);
            SortCoursesByLanguageCommand = new RelayCommand(SortCoursesByLanguage, CanSortCoursesByLanguage);
            SortCoursesByLevelCommand = new RelayCommand(SortCoursesByLevel, CanSortCoursesByLevel);
            ShowCreateCourseCommand = new RelayCommand(ShowCreateCourse, CanCreateCourse);
            ShowUpdateSelectedCourseCommand = new RelayCommand(UpdateSelectedCourse, CanUpdateSelectedCourse);
            ViewCourseApplicationsCommand = new RelayCommand(ViewCourseApplications, CanViewCourseApplications);
            FinishCourseCommand = new RelayCommand(FinishCourse, CanFinishCourse);

            ShowCreateExamCommand = new RelayCommand(ShowCreateExam, CanCreateExam);

            LoadTeacherCourses();
        }

        private bool CanFinishCourse(object obj)
        {
            return true;
        }


        private void FinishCourse(object obj)
        {
            if (SelectedCourse == null)
            {
                MessageBox.Show("Please select a course to finish.");
                return;
            }
            using (var dbContext = new DatabaseContext())
            {
                if (UserSession.Instance.LoggedInUser is Teacher teacher)
                {
                    var course = dbContext.Courses
                        .Include(c => c.Students)
                        .Include(t => t.Teacher)
                        .FirstOrDefault(c => c.TeacherId == teacher.Id);

                    if (course != null)
                    {
                        if (course.Students.Count == course.MaxStudents)
                        {
                            course.IsFinished = true;
                            dbContext.SaveChanges();
                            MessageBox.Show("Course has been marked as finished.");
                        }
                        else
                        {
                            MessageBox.Show("Course is not filled with the maximum number of students.");
                        }
                    }
                }
            }
        }

        private bool CanCreateExam(object obj)
        {
            return true;
        }

        private void ShowCreateExam(object obj)
        {
            CreateExam createExam = new WPFTutorial.View.CreateExam();
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = createExam;
        }

        private void LoadTeacherCourses()
        {
            using (var dbContext = new DatabaseContext())
            {
                if (UserSession.Instance.LoggedInUser is Teacher teacher)
                {
                    var courses = dbContext.Courses
                        .Include(c => c.Teacher)
                        .Where(c => c.TeacherId == teacher.Id)
                        .ToList();

                    TeacherCourses.Clear();
                    foreach (var course in courses)
                    {
                        TeacherCourses.Add(course);
                    }
                }
            }
        }

        private bool CanCreateCourse(object obj)
        {
            return true;
        }

        private void ShowCreateCourse(object obj)
        {
            CreateCourse createCourse = new CreateCourse();
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = createCourse;
        }

        private bool CanSortCoursesByCreationDate(object obj)
        {
            return true;
        }

        private bool CanSortCoursesByLanguage(object obj)
        {
            return true;
        }

        private bool CanSortCoursesByLevel(object obj)
        {
            return true;
        }

        private bool CanUpdateSelectedCourse(object obj)
        {
            return true;
        }

        private void UpdateSelectedCourse(object obj)
        {
            if (SelectedCourse != null)
            {
                UpdateCourse updateCourse = new UpdateCourse(SelectedCourse);
                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Content = updateCourse;
            }
            else
            {
                MessageBox.Show("Please select a course you would like to update");
            }
        }

        private bool CanViewCourseApplications(object obj)
        {
            return true;
        }

        private void ViewCourseApplications(object obj)
        {
            if (SelectedCourse != null)
            {
                MessageBox.Show($"Navigating to applications for course: {SelectedCourse.CourseName}");
                ViewCourseApplications viewCourseApplications = new ViewCourseApplications(SelectedCourse);
                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Content = viewCourseApplications;
            }
            else
            {
                MessageBox.Show("Please select a course to view applications");
            }
        }


        private void SortCoursesByCreationDate(object obj)
        {
            var sortedCourses = TeacherCourses?.OrderBy(c => c.StartsAt).ToList();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
