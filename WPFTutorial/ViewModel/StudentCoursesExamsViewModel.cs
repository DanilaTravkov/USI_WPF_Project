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
    public class StudentCoursesExamsViewModel : INotifyPropertyChanged
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

        public ObservableCollection<Course> AvailableCourses { get; set; }
        public ICommand EnrollCourseCommand { get; set; }
        public ICommand SeeMyApplicationsCommand { get; set; }

        public StudentCoursesExamsViewModel()
        {
            AvailableCourses = new ObservableCollection<Course>();
            EnrollCourseCommand = new RelayCommand(EnrollCourse, CanEnrollCourse);
            SeeMyApplicationsCommand = new RelayCommand(SeeMyApplications, CanSeeMyApplications);

            using (var dbContext = new DatabaseContext())
            {
                if (UserSession.Instance.LoggedInUser is Student student)
                {
                    var courses = dbContext.Courses
                        .Include(c => c.Teacher)
                        .Include(c => c.Students)
                        .ToList();

                    var filteredCourses = courses
                        .Where(c => c.MaxStudents > c.Students.Count)
                        .Where(c => c.StartsAt.HasValue && (c.StartsAt.Value - DateTime.Now).TotalDays > 7)
                        .ToList();

                    AvailableCourses.Clear();
                    foreach (var course in filteredCourses)
                    {
                        AvailableCourses.Add(course);
                    }
                }
            }
        }

        private bool CanSeeMyApplications(object obj)
        {
            return true;
        }

        private void SeeMyApplications(object obj)
        {
            MyApplications myApplications = new WPFTutorial.View.MyApplications();
            Application.Current.MainWindow.Content = myApplications;
        }


        private bool CanEnrollCourse(object obj)
        {
            return true;
        }

        private void EnrollCourse(object obj)
        {
            if (UserSession.Instance.LoggedInUser is Student student)
            {
                if (SelectedCourse != null)
                {
                    using (var dbContext = new DatabaseContext())
                    {
                        if (student.CourseId != null)
                        {
                            MessageBox.Show("You are already enrolled in a course.");
                            return;
                        }

                        var courseApplication = new CourseApplication
                        {
                            StudentId = student.Id,
                            CourseId = SelectedCourse.Id,
                            ApplicationDate = DateTime.Now
                        };

                        dbContext.CourseApplications.Add(courseApplication);
                        dbContext.SaveChanges();

                        MessageBox.Show("You have applied for the course.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a course");
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
