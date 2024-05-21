using System;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;

namespace WPFTutorial.ViewModel
{
    public class RegisterTeacherViewModel
    {
        public ICommand AddTeacherCommand { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Model.Role Role { get; } = Model.Role.TEACHER;

        public RegisterTeacherViewModel()
        {
            AddTeacherCommand = new RelayCommand(AddTeacher, CanAddTeacher);
        }

        private bool CanAddTeacher(object obj)
        {
            return true;
        }

        private void AddTeacher(object obj)
        {
            if (!CanAddTeacher(obj))
            {
                MessageBox.Show("Some input(s) are empty!");
                return;
            }

            var newTeacher = new Teacher(Name, Surname, Email, Password, Gender, DateOfBirth, Role);

            using (var dbContext = new DatabaseContext())
            {
                dbContext.Teachers.Add(newTeacher);
                dbContext.SaveChanges();
                MessageBox.Show("Successfully created a new teacher");
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
