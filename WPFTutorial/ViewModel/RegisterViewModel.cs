using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;

namespace WPFTutorial.ViewModel
{

    // TODO: Change this to LoginViewModel.xaml
    public class RegisterViewModel
    {
        public ICommand AddStudentCommand { get; set; }
        public string? Name {  get; set; }
        public string? Surname {  get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender {  get; set; }
        public DateTime DateOfBirth {  get; set; }
        public Model.Role Role { get; } = Model.Role.STUDENT;

        public RegisterViewModel()
        {
            AddStudentCommand = new RelayCommand(AddStudent, CanAddStudent);
        }

        private bool CanAddStudent(object obj)
        {
            return true;
        }

        private void AddStudent(object obj)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Gender)) {
                MessageBox.Show("Some input(s) are empty!");
            }
            else
            {

                Student newStudent = new Student(Name, Surname, Email, Password, Gender, DateOfBirth, Role); // When a new user is created they don't have any related courses yet

                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Students.Add(newStudent);
                    dbContext.SaveChanges();
                    MessageBox.Show("Succsessfully created a new student");
                }

                Name = null;
                Surname = null;
                Email = null;
                Password = null;
                Gender = null;
                DateOfBirth = default;
            }
        }
    }
}
