using System;
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
    public class CreateExamViewModel : INotifyPropertyChanged
    {
        private string examName;
        private string language;
        private string languageLevel;
        private int maxNumberOfStudents;
        private DateTime examDate;
        private bool isOnline;

        public string ExamName
        {
            get => examName;
            set
            {
                examName = value;
                OnPropertyChanged();
            }
        }

        public string Language
        {
            get => language;
            set
            {
                language = value;
                OnPropertyChanged();
            }
        }

        public string LanguageLevel
        {
            get => languageLevel;
            set
            {
                languageLevel = value;
                OnPropertyChanged();
            }
        }

        public int MaxNumberOfStudents
        {
            get => maxNumberOfStudents;
            set
            {
                maxNumberOfStudents = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExamDate
        {
            get => examDate;
            set
            {
                examDate = value;
                OnPropertyChanged();
            }
        }

        public bool IsOnline
        {
            get => isOnline;
            set
            {
                isOnline = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateExamCommand { get; set; }

        public CreateExamViewModel()
        {
            CreateExamCommand = new RelayCommand(CreateExam, CanCreateExam);
            ExamDate = DateTime.Now;
        }

        private bool CanCreateExam(object parameter)
        {
            // You can add your validation logic here
            return true;
        }

        private void CreateExam(object parameter)
        {
           

            if (UserSession.Instance.IsTeacher())
            {
                var loggedInTeacher = UserSession.Instance.LoggedInUser as Teacher;

                if (loggedInTeacher != null)
                {
                    MessageBox.Show($"Creating exam for teacher with ID: {loggedInTeacher.Id}");

                    using (var dbContext = new DatabaseContext())
                    {
                        // Attach the existing teacher to the context to avoid insertion
                        dbContext.Teachers.Attach(loggedInTeacher);

                        Exam newExam = new Exam
                        {
                            ExamName = ExamName,
                            Language = Language,
                            LanguageLevel = Enum.Parse<ELevel>(LanguageLevel),
                            MaxNumberOfStudents = MaxNumberOfStudents,
                            ExamDate = ExamDate,
                            // TeacherId = loggedInTeacher.Id, // Uncomment and implement if needed
                            // Additional properties can be set here
                        };

                        dbContext.Exams.Add(newExam);
                        dbContext.SaveChanges();
                        MessageBox.Show("Successfully created a new exam");
                    }
                }
                else
                {
                    MessageBox.Show("Logged in user is not a teacher.");
                }
            }
            else
            {
                MessageBox.Show("You must be logged in as a teacher to create an exam.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
