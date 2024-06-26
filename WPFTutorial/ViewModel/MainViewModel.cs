﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public ICommand ShowLoginCommand {  get; set; }

        public MainViewModel() 
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {

                ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow); // We assign an instance of a RelayCommand to the command and pass the action function as the first param
                ShowLoginCommand = new RelayCommand(ShowLogin, CanShowLogin);
            }
        }

        private bool CanShowLogin(object obj)
        {
            return true;
        }

        private bool CanShowWindow(object obj) // We probably won't use this much but its a good idea to keep this structure
        {
            return true;
        }

        private void ShowWindow(object obj) // The action function where lays the logic which will be executed when the command is invoked (for example through a button click)
        {
            TeacherOrStudent teacherOrStudent = new WPFTutorial.View.TeacherOrStudent();

            // Get the main window and set its content to the new view
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = teacherOrStudent;

        }

        // Show login window
        private void ShowLogin(object obj)
        {
           Login login = new Login();
            login.Show();
        }
    }
}
