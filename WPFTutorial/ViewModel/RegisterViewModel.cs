using System;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;

namespace WPFTutorial.ViewModel
{
    // CRUD create for Student
    public class RegisterViewModel
    {
        public ICommand AddStudentCommand { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
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
            if (!CanAddStudent(obj))
            {
                MessageBox.Show("Some input(s) are empty!");
                return;
            }

            var newStudent = new Student(Name, Surname, Email, Password, Gender, DateOfBirth, Role);

            using (var dbContext = new DatabaseContext())
            {
                dbContext.Students.Add(newStudent);
                dbContext.SaveChanges();
                MessageBox.Show("Successfully created a new student");
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
