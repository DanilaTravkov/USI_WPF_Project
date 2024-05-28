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

        private string CourseAvailability = "Available courses for you: ";
        public string _CourseAvailability 
        {  
            get => CourseAvailability;
            set
            {
                CourseAvailability = value;
                OnPropertyChanged();
            }
        }

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
        public ICommand SortCoursesByCreationDateCommand { get; set; }
        public ICommand SortCoursesByCourseNameCommand { get; set; }
        public ICommand SortCoursesByLevelCommand { get; set; }
        public ICommand EnrollCourseCommand { get; set; }
        public ICommand SeeMyApplicationsCommand { get; set; }
        public ICommand SeeMyCourseCommand {  get; set; }

        public StudentCoursesExamsViewModel()
        {
            AvailableCourses = new ObservableCollection<Course>();
            SortCoursesByCreationDateCommand = new RelayCommand(SortByCreationDate, CanSortByCreationDate);
            SortCoursesByCourseNameCommand = new RelayCommand(SortByCourseName, CanSortByCourseName);
            SortCoursesByLevelCommand = new RelayCommand(SortByLevel, CanSortByLevel);

            EnrollCourseCommand = new RelayCommand(EnrollCourse, CanEnrollCourse);
            SeeMyApplicationsCommand = new RelayCommand(SeeMyApplications, CanSeeMyApplications);
            SeeMyCourseCommand = new RelayCommand(SeeMyCourse, CanSeeMyCourse);

            using (var dbContext = new DatabaseContext())
            {
                if (UserSession.Instance.LoggedInUser is Student student)
                {
                    if (student.CourseId != null)
                    {
                        MessageBox.Show("Congratulations, you have been accepted on a course!");
                        CourseAvailability = "You have already been accepted on a course, click 'See my course' to view data";
                        AvailableCourses.Clear();
                    }
                    else
                    {
                        var courses = dbContext.Courses
                            .Include(c => c.Teacher)
                            .Include(c => c.Students)
                            .ToList();

                        var filteredCourses = courses // display available courses for a student
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
        }

        private bool CanSortByCreationDate(object obj)
        {
            return true; // Add your condition if necessary
        }

        private void SortByCreationDate(object obj)
        {
            // Add sorting logic by creation date here
        }

        private bool CanSortByCourseName(object obj)
        {
            return true; // Add your condition if necessary
        }

        private void SortByCourseName(object obj)
        {
            // Add sorting logic by course name here
        }

        private bool CanSortByLevel(object obj)
        {
            return true; // Add your condition if necessary
        }

        private void SortByLevel(object obj)
        {
            // Add sorting logic by level here
        }


        private bool CanSeeMyCourse(object obj)
        {
            return true;
        }

        private void SeeMyCourse(object obj)
        {
            StudentSeeCoursesData courseData = new WPFTutorial.View.StudentSeeCoursesData();
            Application.Current.MainWindow.Content = courseData;
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
                using (var dbContext = new DatabaseContext())
                {
                    // Check if the student is already enrolled in the selected course
                    if (student.CourseId == SelectedCourse.Id)
                    {
                        MessageBox.Show("You are already enrolled in this course.");
                        return;
                    }

                    // Check if the student already has an application for the selected course
                    var existingApplication = dbContext.CourseApplications
                        .FirstOrDefault(ca => ca.StudentId == student.Id && ca.CourseId == SelectedCourse.Id);

                    if (existingApplication != null)
                    {
                        MessageBox.Show("You have already applied for this course.");
                        return;
                    }

                    if (SelectedCourse != null)
                    {
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
                    else
                    {
                        MessageBox.Show("Please select a course");
                    }
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
