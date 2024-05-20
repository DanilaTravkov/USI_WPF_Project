using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTutorial.Model;

namespace WPFTutorial.Session
{
    // Since WPF does not have sessions we create one using a singleton pattern (there can only be one instance of UserSession object per launch)
    public class UserSession
    {
        private static UserSession _Instance;
        public static UserSession Instance => _Instance ??= new UserSession();

        public User LoggedInUser { get; private set; }

        private UserSession() { }

        public void SetLoggedInUser(User user)
        {
            LoggedInUser = user;
        }

        public bool IsTeacher()
        {
            return LoggedInUser != null && LoggedInUser.Role == Role.TEACHER;
        }

        public bool IsStudent()
        {
            return LoggedInUser != null && LoggedInUser.Role == Role.STUDENT;
        }
    }

}
