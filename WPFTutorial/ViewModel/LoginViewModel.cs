using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFTutorial.Commands;

namespace WPFTutorial.ViewModel
{
    public class LoginViewModel
    {
        public ICommand SubmitLoginCommand { get; set; } // Instead of events we use commands

        public LoginViewModel() 
        {
            SubmitLoginCommand = new RelayCommand(Submit, CanSubmit);
        }

        private void Submit(object obj)
        {
            MessageBox.Show("LOGIC ATTEMPTED LMAO");
        }

        private bool CanSubmit(object obj)
        {
            return true;
        }
    }
}
