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
                var user = dbContext.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);
                if (user != null)
                {
                    UserSession.Instance.SetLoggedInUser(user);
                    MessageBox.Show("Login successful!");
                    // Navigate to the appropriate view
                }
                else
                {
                    MessageBox.Show("Invalid credentials");
                }
            }
        }

        private bool CanSubmit(object obj)
        {
            return true; // You can add validation logic here
        }
    }

}
