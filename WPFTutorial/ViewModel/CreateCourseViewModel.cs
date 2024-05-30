using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.DB;
using WPFTutorial.Model;
using WPFTutorial.Session;

namespace WPFTutorial.ViewModel
{
    public class CreateCourseViewModel : INotifyPropertyChanged
    {
        private ELevel courseLevel;
        private int? weeksDuration;
        private ObservableCollection<WeekDays> weekDays;
        private ObservableCollection<WeekDays> selectedDays;
        private DateTime? startsAt;
        private bool? isOnline;
        private int? maxStudents;
        private string courseName;
        private string language;

        public string Language
        {
            get => language;
            set
            {
                language = value;
                OnPropertyChanged();
            }
        }

        public ELevel CourseLevel
        {
            get => courseLevel;
            set
            {
                courseLevel = value;
                OnPropertyChanged();
            }
        }

        public int? WeeksDuration
        {
            get => weeksDuration;
            set
            {
                weeksDuration = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WeekDays> WeekDays
        {
            get => weekDays;
            set
            {
                weekDays = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WeekDays> SelectedDays
        {
            get => selectedDays;
            set
            {
                selectedDays = value;
                OnPropertyChanged();
            }
        }

        public DateTime? StartsAt
        {
            get => startsAt;
            set
            {
                startsAt = value;
                OnPropertyChanged();
            }
        }

        public bool? IsOnline
        {
            get => isOnline;
            set
            {
                isOnline = value;
                OnPropertyChanged();
            }
        }

        public int? MaxStudents
        {
            get => maxStudents;
            set
            {
                maxStudents = value;
                OnPropertyChanged();
            }
        }

        public string CourseName
        {
            get => courseName;
            set
            {
                courseName = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateCourseCommand { get; set; }

        public CreateCourseViewModel()
        {
            CreateCourseCommand = new RelayCommand(CreateCourse, CanCreateCourse);
            WeekDays = new ObservableCollection<WeekDays>(Enum.GetValues(typeof(WeekDays)).Cast<WeekDays>());
            SelectedDays = new ObservableCollection<WeekDays>();
        }

        private bool CanCreateCourse(object obj)
        {
            return UserSession.Instance.IsTeacher();
        }

        private void CreateCourse(object obj)
        {
            if (string.IsNullOrEmpty(CourseName) || WeeksDuration <= 0 || WeeksDuration == null || StartsAt == null)
            {
                MessageBox.Show("Some input(s) are empty!");
                return;
            }

            if (UserSession.Instance.IsTeacher())
            {
                var loggedInTeacher = UserSession.Instance.LoggedInUser as Teacher;

                if (loggedInTeacher != null)
                {
                    MessageBox.Show($"Creating course for teacher with ID: {loggedInTeacher.Id}");

                    using (var dbContext = new DatabaseContext())
                    {
                        // Attach the existing teacher to the context to avoid insertion
                        dbContext.Teachers.Attach(loggedInTeacher);

                        Course newCourse = new Course
                        {
                            TeacherId = loggedInTeacher.Id, // Ensure TeacherId is set
                            CourseLevel = CourseLevel, // TODO: Fix A1 being assigned to every course ingoring user's choice
                            WeeksDuration = WeeksDuration,
                            StartsAt = StartsAt,
                            IsOnline = IsOnline,
                            MaxStudents = MaxStudents,
                            CourseName = CourseName,
                            Language = Language,
                            WeekDays = SelectedDays.ToList()
                        };

                        dbContext.Courses.Add(newCourse);
                        dbContext.SaveChanges();
                        MessageBox.Show("Successfully created a new course");
                    }
                }
                else
                {
                    MessageBox.Show("Logged in user is not a teacher.");
                }
            }
            else
            {
                MessageBox.Show("You must be logged in as a teacher to create a course.");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
