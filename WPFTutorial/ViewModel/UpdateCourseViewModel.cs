using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;

namespace WPFTutorial.ViewModel
{
    public class UpdateCourseViewModel : INotifyPropertyChanged
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

        public ICommand UpdateCourseCommand { get; set; }

        public UpdateCourseViewModel(Course course)
        {
            SelectedCourse = course;
            UpdateCourseCommand = new RelayCommand(UpdateCourse, CanUpdateCourse);
        }

        private bool CanUpdateCourse(object obj)
        {
            return true;
        }

        private void UpdateCourse(object obj)
        {
            using (var dbContext = new DatabaseContext())
            {
                dbContext.Courses.Update(SelectedCourse);
                dbContext.SaveChanges();
                MessageBox.Show("Course updated successfully.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
