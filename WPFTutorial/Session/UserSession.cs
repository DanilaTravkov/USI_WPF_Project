using WPFTutorial.Model;

namespace WPFTutorial.Session
{
    public class UserSession
    {
        private static UserSession _instance;
        public static UserSession Instance => _instance ??= new UserSession();

        public object LoggedInUser { get; private set; }

        private UserSession() { }

        public void SetLoggedInUser(object user)
        {
            LoggedInUser = user;
        }

        public bool IsTeacher()
        {
            return LoggedInUser is Teacher;
        }

        public bool IsStudent()
        {
            return LoggedInUser is Student;
        }
    }
}
