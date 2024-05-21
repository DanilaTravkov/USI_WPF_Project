using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    // Student 1, 2
    public class StudentCoursesExamsViewModel
    {
        public ObservableCollection<Course> AvailableCourses { get; set; }

        public StudentCoursesExamsViewModel()
        {
            AvailableCourses = new ObservableCollection<Course>();

            using (var dbContext = new DatabaseContext())
            {
                if (UserSession.Instance.LoggedInUser is Student student)
                {
                    // Fetch all courses with related data
                    var courses = dbContext.Courses
                        .Include(c => c.Teacher)
                        .Include(c => c.Students)
                        .ToList(); // Execute query and fetch data

                    // Filter the courses in memory
                    var filteredCourses = courses
                        .Where(c => c.MaxStudents > c.Students.Count) // Number of registered students is less than the max number
                        .Where(c => c.StartsAt.HasValue && (c.StartsAt.Value - DateTime.Now).TotalDays > 7) // Starts in more than 7 days
                        .ToList();

                    AvailableCourses.Clear();
                    foreach (var course in filteredCourses)
                    {
                        AvailableCourses.Add(course);
                    }
                }
            }
        }
        // TODO: add a command and a function to entroll on a selected course
    }
}
