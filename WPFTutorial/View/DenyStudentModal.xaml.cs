using System.Windows;
using WPFTutorial.ViewModel;

namespace WPFTutorial.View
{
    public partial class DenyStudentModal : Window
    {
        public DenyStudentModal()
        {
            InitializeComponent();
            var viewModel = new DenyStudentModalViewModel();
            viewModel.RequestClose += (s, e) => this.DialogResult = true;
            DataContext = viewModel;
        }

        public string DenialMessage => (DataContext as DenyStudentModalViewModel).DenialMessage;
    }
}
