using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class LoginViewModel
    {
        public ICommand SubmitLoginCommand { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public LoginViewModel()
        {
            SubmitLoginCommand = new RelayCommand(Submit, CanSubmit);
        }

        private void Submit(object obj)
        {
            using (var dbContext = new DatabaseContext())
            {
                var teacher = dbContext.Teachers.FirstOrDefault(t => t.Email == Email && t.Password == Password);
                var student = dbContext.Students.FirstOrDefault(s => s.Email == Email && s.Password == Password);

                if (teacher != null)
                {
                    UserSession.Instance.SetLoggedInUser(teacher);
                    MessageBox.Show("Login successful!");

                    // Create the TeacherCoursesExams view if the logged-in user is a teacher
                    var teacherCoursesExamsView = new WPFTutorial.View.TeacherCoursesExams();
                    Window mainWindow = Application.Current.MainWindow;
                    mainWindow.Content = teacherCoursesExamsView;
                }
                else if (student != null)
                {
                    UserSession.Instance.SetLoggedInUser(student);
                    MessageBox.Show("Login successful!");

                    // Handle student-specific logic if needed
                }
                else
                {
                    MessageBox.Show("Invalid credentials");
                }
            }
        }

        private bool CanSubmit(object obj)
        {
            return true;
        }
    }
}
