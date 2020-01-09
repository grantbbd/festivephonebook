using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Festive_Phonebook_App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string EmailAddress { get; set; }
        public Command CheckEmailCommand { get; set; }

        public LoginViewModel()
        {
            CheckEmailCommand = new Command(async () => await ExecuteCheckEmailCommand());
        }

        async Task ExecuteCheckEmailCommand()
        {

        }
    }
}
