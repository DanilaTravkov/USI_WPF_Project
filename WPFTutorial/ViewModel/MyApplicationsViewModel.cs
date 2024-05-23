using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class ViewMyApplicationsViewModel
    {
        public ObservableCollection<CourseApplication> MyApplications { get; set; }

        public ViewMyApplicationsViewModel()
        {
            MyApplications = new ObservableCollection<CourseApplication>();

            LoadApplications();
        }

        private void LoadApplications()
        {
            using (var dbContext = new DatabaseContext())
            {
                if (UserSession.Instance.LoggedInUser is Student student)
                {
                    var applications = dbContext.CourseApplications
                        .Include(ca => ca.Course)
                        .ThenInclude(c => c.Teacher)
                        .Where(ca => ca.StudentId == student.Id)
                        .ToList();

                    MyApplications.Clear();
                    foreach (var application in applications)
                    {
                        MyApplications.Add(application);
                    }
                }
            }
        }
    }
}
