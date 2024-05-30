using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.View;

namespace WPFTutorial.ViewModel
{
    public class ViewCourseApplicationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CourseApplication> CourseApplications { get; set; }
        public CourseApplication SelectedApplication { get; set; }
        public ICommand AcceptStudentCommand { get; set; }
        public ICommand DenyStudentCommand { get; set; }

        private Course selectedCourse;
        public Course SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
                OnPropertyChanged();
                LoadCourseApplications();
            }
        }

        public ViewCourseApplicationsViewModel(Course course)
        {
            CourseApplications = new ObservableCollection<CourseApplication>();
            AcceptStudentCommand = new RelayCommand(AcceptStudent, CanExecuteAcceptDeny);
            DenyStudentCommand = new RelayCommand(DenyStudent, CanExecuteAcceptDeny);
            SelectedCourse = course;
            LoadCourseApplications();
        }

        private bool CanExecuteAcceptDeny(object parameter)
        {
            return true;
        }

        private void LoadCourseApplications()
        {
            CourseApplications.Clear();
            using (var dbContext = new DatabaseContext())
            {
                var courseApplications = dbContext.CourseApplications
                    .Where(ca => ca.CourseId == SelectedCourse.Id)
                    .Include(ca => ca.Student)
                    .ToList();
                foreach (var application in courseApplications)
                {
                    CourseApplications.Add(application);
                }
            }
        }


        private void AcceptStudent(object parameter)
        {
            if (SelectedApplication != null)
            {
                using (var dbContext = new DatabaseContext())
                {
                    var student = dbContext.Students.FirstOrDefault(s => s.Id == SelectedApplication.StudentId);
                    var application = dbContext.CourseApplications.FirstOrDefault(ca => ca.Id == SelectedApplication.Id);

                    // Check if the student exists and is not already enrolled in a course
                    if (student != null && student.CourseId == null)
                    {
                        // Assign the course to the student
                        student.CourseId = SelectedApplication.CourseId;
                        application.IsAccepted = true;

                        // Increase counter of students on course;
                        var course = dbContext.Courses.FirstOrDefault(c => student.CourseId == c.Id);
                        course.StudentsCount++;

                        // Remove the accepted application
                        dbContext.CourseApplications.Remove(application);

                        // Remove other pending applications for the student
                        var otherApplications = dbContext.CourseApplications
                            .Where(ca => ca.StudentId == student.Id && ca.Id != SelectedApplication.Id && !ca.IsAccepted)
                            .ToList();
                        dbContext.CourseApplications.RemoveRange(otherApplications);

                        dbContext.SaveChanges();

                        MessageBox.Show("Student accepted");

                        // Refresh the list
                        LoadCourseApplications();
                    }
                    else if (student != null && student.CourseId != null)
                    {
                        MessageBox.Show("This student already has a course");
                    }
                    else
                    {
                        MessageBox.Show("Student not found or already has a course");
                    }
                }
            }
            else
            {
                MessageBox.Show("No application selected.");
            }
        }


        private void DenyStudent(object parameter)
        {
            if (SelectedApplication != null)
            {
                DenyStudentModal denyModal = new DenyStudentModal();
                bool? result = denyModal.ShowDialog();

                if (result == true)
                {
                    using (var dbContext = new DatabaseContext())
                    {
                        var application = dbContext.CourseApplications
                            .FirstOrDefault(ca => ca.Id == SelectedApplication.Id);

                        if (application != null)
                        {
                            application.IsAccepted = false;
                            application.DenialMessage = denyModal.DenialMessage;
                            dbContext.SaveChanges();

                            // Refresh the list
                            LoadCourseApplications();
                        }
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
