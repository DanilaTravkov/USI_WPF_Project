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
                var teacher = dbContext.Teachers
                    .FirstOrDefault(t => t.Email.ToLower() == Email.ToLower() && t.Password == Password);
                var student = dbContext.Students
                    .FirstOrDefault(s => s.Email.ToLower() == Email.ToLower() && s.Password == Password);
                var director = dbContext.Directors
                    .FirstOrDefault(d => d.Email.ToLower() == Email.ToLower() && d.Password == Password);

                if (teacher != null)
                {
                    UserSession.Instance.SetLoggedInUser(teacher);
                    MessageBox.Show("Succsessfully logged in as TEACHER");

                    // Create the TeacherCoursesExams view if the logged-in user is a teacher
                    var teacherCoursesExamsView = new WPFTutorial.View.TeacherCoursesExams();
                    Window mainWindow = Application.Current.MainWindow;
                    mainWindow.Content = teacherCoursesExamsView;
                }
                else if (student != null)
                {
                    UserSession.Instance.SetLoggedInUser(student);
                    MessageBox.Show("Succsessfully logged in as STUDENT");

                    var studentCoursesExamsView = new WPFTutorial.View.StudentCoursesExams();
                    Window mainWindow = Application.Current.MainWindow;
                    mainWindow.Content = studentCoursesExamsView;
                }
                else if (director != null)
                {
                    UserSession.Instance.SetLoggedInUser(director);
                    MessageBox.Show("Succsessfully logged in as DIRECTOR");

                    var directorMainView = new WPFTutorial.View.DirectorMainView();
                    Window mainWindow = Application.Current.MainWindow;
                    mainWindow.Content = directorMainView;
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
