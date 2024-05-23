using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFTutorial.Commands;

namespace WPFTutorial.ViewModel
{
    public class DenyStudentModalViewModel : INotifyPropertyChanged
    {
        private string denialMessage;
        public string DenialMessage
        {
            get => denialMessage;
            set
            {
                denialMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelCommand { get; set; }
        public ICommand DenyCommand { get; set; }

        public event EventHandler RequestClose;

        public DenyStudentModalViewModel()
        {
            CancelCommand = new RelayCommand(Cancel, CanCancel);
            DenyCommand = new RelayCommand(Deny, CanDeny);
        }

        private void Cancel(object obj)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private bool CanCancel(object obj)
        {
            return true;
        }

        private bool CanDeny(object obj)
        {
            return true;
        }

        private void Deny(object obj)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
