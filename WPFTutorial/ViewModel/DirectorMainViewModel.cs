using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.View;

namespace WPFTutorial.ViewModel
{
    public class DirectorMainViewModel
    {
        public ICommand ShowCreateExamCommand { get; set; }

        public ICommand ShowTeachersAssignedToExamsCommand {  get; set; }

        public DirectorMainViewModel()
        {
            ShowCreateExamCommand = new RelayCommand(ShowCreateExam, CanShowCreateExam);
            ShowTeachersAssignedToExamsCommand = new RelayCommand(ShowTeachersAssignedToExams, CanShowTeachersAssignedToExams);
        }

        private bool CanShowTeachersAssignedToExams(object obj)
        {
            return true;
        }

        private void ShowTeachersAssignedToExams(object obj)
        {
            DirectorSystemAssignedTeacherToExam directorSystemAssignedTeacherToExam = new View.DirectorSystemAssignedTeacherToExam();
            Application.Current.MainWindow.Content = directorSystemAssignedTeacherToExam;
        }

        private bool CanShowCreateExam(object obj)
        {
            return true;
        }
        
        private void ShowCreateExam(object obj)
        {
            MessageBox.Show("Showing create exam for director");
            DirectorCreateExam directorCreateExam = new View.DirectorCreateExam();
            Application.Current.MainWindow.Content = directorCreateExam;
        }
    }
}
