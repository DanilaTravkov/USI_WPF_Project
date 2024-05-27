using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFTutorial.Commands;
using WPFTutorial.Model;

namespace WPFTutorial.ViewModel
{
    public class CreateExamViewModel : INotifyPropertyChanged
    {
        private string examName;
        public string ExamName
        {
            get => examName;
            set
            {
                examName = value;
                OnPropertyChanged();
            }
        }

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

        private int maxNumberOfStudents;
        public int MaxNumberOfStudents
        {
            get => maxNumberOfStudents;
            set
            {
                maxNumberOfStudents = value;
                OnPropertyChanged();
            }
        }

        private DateTime examDate = DateTime.Now;
        public DateTime ExamDate
        {
            get => examDate;
            set
            {
                examDate = value;
                OnPropertyChanged();
            }
        }

        private bool isOnline;
        public bool IsOnline
        {
            get => isOnline;
            set
            {
                isOnline = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> languageLevels;
        public ObservableCollection<string> LanguageLevels
        {
            get => languageLevels;
            set
            {
                languageLevels = value;
                OnPropertyChanged();
            }
        }

        private string selectedLanguageLevel;
        public string SelectedLanguageLevel
        {
            get => selectedLanguageLevel;
            set
            {
                selectedLanguageLevel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateExamCommand { get; set; }

        public CreateExamViewModel()
        {
            // Пример заполнения списка уровней языка
            LanguageLevels = new ObservableCollection<string>
            {
                "Beginner",
                "Intermediate",
                "Advanced"
            };

            CreateExamCommand = new RelayCommand(CreateExam, CanCreateExam);
        }

        private bool CanCreateExam(object obj)
        {
            return true;
        }

        private void CreateExam(object parameter)
        {
            // Здесь добавьте логику для создания экзамена
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
