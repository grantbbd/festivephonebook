using Festive_Phonebook_App.Models;
using Festive_Phonebook_App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Festive_Phonebook_App.ViewModels
{
    public class PhonebookEntryViewModel : BaseViewModel
    {
        public IPhonebookService PhonebookService => DependencyService.Get<IPhonebookService>();
        public PhoneBookEntry BookEntry { get; set; }
        private INavigation Navigation;
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SetNaughtyCommand { get; set; }
        public ICommand SetNiceCommand { get; set; }
        private bool isNice;
        public bool IsNice
        {
            get { return isNice; }
            set { SetProperty(ref isNice, value); }
        }
        private bool isNaughty;
        public bool IsNaughty
        {
            get { return isNaughty; }
            set { SetProperty(ref isNaughty, value); }
        }

        public PhonebookEntryViewModel(PhoneBookEntry entry, INavigation navigation)
        {
            BookEntry = entry;
            Navigation = navigation;

            CancelCommand = new Command(async () => await Navigation.PopModalAsync() );
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            SetNaughtyCommand = new Command(() => {
                IsNaughty = true;
                IsNice = false;
            });
            SetNiceCommand = new Command(() => {
                IsNaughty = false;
                IsNice = true;
            });

            IsNice = entry.Kind == 1;
            IsNaughty = entry.Kind == 0;
        }

        async Task ExecuteSaveCommand()
        {
            string token = await SecureStorage.GetAsync("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                BookEntry.Kind = IsNice ? 1 : 0;
                var result = await PhonebookService.CreateEntry(token, BookEntry);
                if (result)
                {
                    MessagingCenter.Send(this, "EditEntry", BookEntry);
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}

