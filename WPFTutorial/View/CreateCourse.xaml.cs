using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTutorial.ViewModel;

namespace WPFTutorial.View
{
    /// <summary>
    /// Interaction logic for CreateCourse.xaml
    /// </summary>
    public partial class CreateCourse : UserControl
    {
        public CreateCourse()
        {
            InitializeComponent();
            CreateCourseViewModel createCourseViewModel = new CreateCourseViewModel();
            this.DataContext = createCourseViewModel;
        }
    }
}
