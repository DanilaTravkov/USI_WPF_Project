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
using WPFTutorial.View;

namespace WPFTutorial.ViewModel
{
    public class ViewCourseApplicationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CourseApplication> CourseApplications { get; set; }
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
            if (parameter is CourseApplication application)
            {
                using (var dbContext = new DatabaseContext())
                {
                    var student = dbContext.Students.FirstOrDefault(s => s.Id == application.StudentId);
                    if (student != null && student.CourseId == null)
                    {
                        student.CourseId = application.CourseId;
                        application.IsAccepted = true;
                        dbContext.SaveChanges();
                        // Notify the student about acceptance
                    }
                }
            }
        }

        private void DenyStudent(object parameter)
        {
            if (SelectedCourse != null)
            {
                DenyStudentModal denyModal = new DenyStudentModal();
                bool? result = denyModal.ShowDialog();

                if (result == true)
                {
                    using (var dbContext = new DatabaseContext())
                    {
                        var application = dbContext.CourseApplications
                            .FirstOrDefault(ca => ca.Id == SelectedCourse.Id);

                        if (application != null)
                        {
                            application.IsAccepted = false;
                            application.DenialMessage = denyModal.DenialMessage;
                            dbContext.SaveChanges();
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
