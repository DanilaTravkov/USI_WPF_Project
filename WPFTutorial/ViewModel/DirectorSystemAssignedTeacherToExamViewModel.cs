using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFTutorial.DB;
using WPFTutorial.Model;

namespace WPFTutorial.ViewModel
{
    public class DirectorSystemAssignedTeacherToExamViewModel
    {
        public ObservableCollection<dynamic> TeachersWithMatchingCourseExamNames { get; set; } = new ObservableCollection<dynamic>();

        public DirectorSystemAssignedTeacherToExamViewModel()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                // Загружаем курсы с языком
                var coursesWithLanguage = dbContext.Courses.Where(c => !string.IsNullOrEmpty(c.Language)).ToList();
                MessageBox.Show($"Courses with Language: {coursesWithLanguage.Count}");
                foreach (var course in coursesWithLanguage)
                {
                    MessageBox.Show($"Course: {course.Id}, Language: {course.Language}");
                }

                // Загружаем все экзамены
                var exams = dbContext.Exams.ToList();
                MessageBox.Show($"Exams: {exams.Count}");
                foreach (var exam in exams)
                {
                    MessageBox.Show($"Exam: {exam.Id}, ExamName: {exam.ExamName}");
                }

                // Проводим соответствие курсов и экзаменов по языку с использованием метода Contains для частичного совпадения
                var matchingCourses = from course in coursesWithLanguage
                                      from exam in exams
                                      where exam.ExamName.ToLower().Contains(course.Language.ToLower())
                                      select new
                                      {
                                          Course = course,
                                          Exam = exam
                                      };

                MessageBox.Show($"Matching Courses and Exams: {matchingCourses.Count()}");
                foreach (var match in matchingCourses)
                {
                    MessageBox.Show($"Course: {match.Course.Id}, Language: {match.Course.Language}, Exam: {match.Exam.Id}, ExamName: {match.Exam.ExamName}");
                }

                // Получаем учителей
                var teachersWithMatchingCourses = from match in matchingCourses
                                                  join teacher in dbContext.Teachers on match.Course.TeacherId equals teacher.Id
                                                  select new
                                                  {
                                                      TeacherId = teacher.Id,
                                                      TeacherName = teacher.Name,
                                                      CourseLanguage = match.Course.Language,
                                                      ExamName = match.Exam.ExamName
                                                  };

                var matchingTeachersList = teachersWithMatchingCourses.ToList();
                MessageBox.Show($"Matching Teachers: {matchingTeachersList.Count}");
                foreach (var teacher in matchingTeachersList)
                {
                    MessageBox.Show($"Teacher: {teacher.TeacherId}, Name: {teacher.TeacherName}, CourseLanguage: {teacher.CourseLanguage}, ExamName: {teacher.ExamName}");
                }

                if (matchingTeachersList.Any())
                {
                    foreach (var item in matchingTeachersList)
                    {
                        TeachersWithMatchingCourseExamNames.Add(item);
                    }
                    MessageBox.Show("Teachers added");
                }
                else
                {
                    MessageBox.Show("No matching teachers found.");
                }
            }
        }



    }
}
