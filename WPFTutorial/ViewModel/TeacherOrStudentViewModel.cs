using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.View;

namespace WPFTutorial.ViewModel
{
    public class TeacherOrStudentViewModel
    {
        public ICommand RegisterAsStudentCommand { get; set; } // Instead of events we use commands
        public ICommand RegisterAsTeacherCommand { get; set; }

        public TeacherOrStudentViewModel() 
        {
            RegisterAsStudentCommand = new RelayCommand(ShowRegisterAsStudent, CanShowRegisterAsStudent);
            RegisterAsTeacherCommand = new RelayCommand(ShowRegisterAsTeacher, CanShowRegisterAsTeacher);
        }

        // TODO: show different register windows for students and teacher;
        private void ShowRegisterAsTeacher(object obj)
        {
            RegisterTeacher registerTeacher = new RegisterTeacher();
            registerTeacher.Show();
        }

        // TODO: show different register windows for students and teacher;
        private void ShowRegisterAsStudent(object obj)
        {
            AddUser register = new AddUser();
            register.Show();
        }

        private bool CanShowRegisterAsTeacher(object obj)
        {
            return true;
        }

        private bool CanShowRegisterAsStudent(object obj)
        {
            return true;
        }


    }
}
