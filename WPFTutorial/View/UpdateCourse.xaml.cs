using System.Windows.Controls;
using WPFTutorial.ViewModel;
using WPFTutorial.Model;

namespace WPFTutorial.View
{
    /// <summary>
    /// Interaction logic for UpdateCourse.xaml
    /// </summary>
    public partial class UpdateCourse : UserControl
    {
        public UpdateCourse(Course course)
        {
            InitializeComponent();
            UpdateCourseViewModel updateCourseViewModel = new UpdateCourseViewModel(course);
            this.DataContext = updateCourseViewModel;
        }
    }
}
