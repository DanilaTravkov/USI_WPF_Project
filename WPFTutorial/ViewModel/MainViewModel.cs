using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.View;

namespace WPFTutorial.ViewModel
{

    // Entry point of the application
    public class MainViewModel
    {
        public ObservableCollection<User> Users { get; set; }

        public ICommand ShowWindowCommand { get; set; } // Instead of events we use commands

        public MainViewModel() 
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                Users = new ObservableCollection<User>(dbContext.Users.ToList()); // Display all users on the MainWindow.xaml

                ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow); // We assign an instance of a RelayCommand to the command and pass the action function as the first param
            }
        }

        private bool CanShowWindow(object obj) // We probably won't use this much but its a good idea to keep this structure
        {
            return true;
        }

        private void ShowWindow(object obj) // The action function where lays the logic which will be executed when the command is invoked (for example through a button click)
        {
            AddUser addUserWindow = new AddUser();
            addUserWindow.Show();
        }
    }
}
