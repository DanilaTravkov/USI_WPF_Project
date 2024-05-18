using System.ComponentModel;
using System.Windows;
using WPFTutorial.ViewModel;

namespace WPFTutorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel; // Another big advantage of MVVM, almost nothing happens in code-behind, we only bind data with the corresponding ViewModel 
        }
    }
}