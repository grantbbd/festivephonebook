using Festive_Phonebook_App.Services;
using Festive_Phonebook_App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Festive_Phonebook_App.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public IPhonebookService PhonebookService => DependencyService.Get<IPhonebookService>();
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Command CheckEmailCommand { get; set; }
        bool _passwordEnabled = false;
        private INavigation Navigation;
        public bool PasswordEnabled
        {
            get { return _passwordEnabled; }
            set { SetProperty(ref _passwordEnabled, value); }
        }

        public LoginViewModel(INavigation navigation)
        {
            CheckEmailCommand = new Command(async () => await ExecuteCheckEmailCommand());
            PasswordEnabled = false;
            IsBusy = false;
            Navigation = navigation;
        }

        async Task ExecuteCheckEmailCommand()
        {
            if (PhonebookService != null)
            {
                IsBusy = true;
                // If password not visible, then confirm the user exists first
                if (!PasswordEnabled)
                {
                    var result = await PhonebookService.UserExists(EmailAddress);
                    if (result)
                    {
                        PasswordEnabled = true;

                    }
                } else
                {
                    // Try to login
                    var token = await PhonebookService.LoginUser(EmailAddress, Password);
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        await SecureStorage.SetAsync("token", token);
                        App.Current.MainPage = new PhonebookPage();
                    }
                }
                IsBusy = false;
            }
        }
    }
}
