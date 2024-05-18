using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WPFTutorial.DB;

namespace WPFTutorial
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e) // override application start-up method to ensure that the db file has already been created, if not create a new db file
        {
            DatabaseFacade facade = new DatabaseFacade(new DatabaseContext());
            facade.EnsureCreated();
        }
    }

}
