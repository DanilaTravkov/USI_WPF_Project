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
    public class AddUserViewModel
    {

        public ICommand AddUserCommand { get; set; }
        public string? Name {  get; set; }
        public string? Email { get; set; }

        public AddUserViewModel()
        {
            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
        }

        private bool CanAddUser(object obj)
        {
            return true;
        }

        private void AddUser(object obj)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email)) {
                MessageBox.Show("Some input(s) are empty!");
            }
            else
            {

                User newUser = new User { Name = Name, Email = Email };

                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();
                    MessageBox.Show("Succsessfully created a new user");
                }

                Name = null;
                Email = null;
            }
        }
    }
}
