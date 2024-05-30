using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class DirectorCreateExamViewModel : INotifyPropertyChanged
    {
        private string examName;
        private string language;
        private string languageLevel;
        private int maxNumberOfStudents;
        private DateTime examDate;
        private bool isOnline;

        public List<string> LanguageLevels { get; } = new List<string> { "A1", "A2", "B1", "B2", "C1", "C2" };

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

        public DirectorCreateExamViewModel()
        {
            CreateExamCommand = new RelayCommand(CreateExam, CanCreateExam);
            ExamDate = DateTime.Now;
        }

        private bool CanCreateExam(object parameter)
        {
            return true;
        }


        private void CreateExam(object parameter)
        {
            if (UserSession.Instance.IsTeacher())
            {
                var loggedInDirector = UserSession.Instance.LoggedInUser as Director;

                if (loggedInDirector != null)
                {
                    if (string.IsNullOrEmpty(ExamName) || string.IsNullOrEmpty(Language))
                    {
                        MessageBox.Show("Some field(s) are empty!");
                        return;
                    }
                    MessageBox.Show($"Creating exam for teacher with ID: {loggedInDirector.Id}");

                    string selectedLanguageLevel = LanguageLevel;

                    if (string.IsNullOrEmpty(selectedLanguageLevel))
                    {
                        throw new ArgumentException("LanguageLevel cannot be null or empty.");
                    }

                    ELevel level;
                    if (!Enum.TryParse(selectedLanguageLevel, out level))
                    {
                        throw new ArgumentException("Invalid language level.");
                    }

                    using (var dbContext = new DatabaseContext())
                    {
                        // Check for existing exam with the same date
                        var existingExam = dbContext.Exams.FirstOrDefault(exam => exam.ExamDate == ExamDate);
                        if (existingExam != null)
                        {
                            MessageBox.Show("An exam with the same date already exists. Please choose a different date.");
                            return;
                        }

                        // Attach the existing director to the context to avoid insertion
                        dbContext.Directors.Attach(loggedInDirector);

                        Exam newExam = new Exam
                        {
                            ExamName = ExamName,
                            Language = Language,
                            LanguageLevel = level,
                            MaxNumberOfStudents = MaxNumberOfStudents,
                            ExamDate = ExamDate,
                            CreatorId = loggedInDirector.Id,
                            CreatorType = "Director"
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
