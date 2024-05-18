using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTutorial.Commands
{
    // This is something used instead of events when implementing MVVM, find out more at https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/commanding-overview?view=netframeworkdesktop-4.8#commands_at_10000_feet
    public class RelayCommand : ICommand // All command classes have to implement an interface ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action<object> _Execute {  get; set; } // A method which we invoke somewhere in our code

        private Predicate<object> _CanExecute { get; set; } // Before a method is invoked we check it with a Predicate type

        public RelayCommand(Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod) 
        {
            _Execute = ExecuteMethod;
            _CanExecute = CanExecuteMethod;

        }

        public bool CanExecute(object? parameter) // First thing to get called when a command is invoked
        {
            return _CanExecute(parameter);
        }

        public void Execute(object? parameter) // Second thing to get called when a command is invoked
        {
            _Execute(parameter);
        }
    }
}
