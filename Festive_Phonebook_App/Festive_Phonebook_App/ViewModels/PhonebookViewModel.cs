using Festive_Phonebook_App.Models;
using Festive_Phonebook_App.Services;
using Festive_Phonebook_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Festive_Phonebook_App.ViewModels
{
    public class PhonebookViewModel : BaseViewModel
    {
        public IPhonebookService PhonebookService => DependencyService.Get<IPhonebookService>();
        public ObservableCollection<PhoneBookEntry> Entries { get; set; }
        private INavigation Navigation;
        public ICommand LoadItemsCommand { get; set; }
        public ICommand AddNaughtyCommand { get; set; }
        public ICommand AddNiceCommand { get; set; }

        public PhonebookViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Entries = new ObservableCollection<PhoneBookEntry>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);

            AddNaughtyCommand = new Command(async () => await ExecuteAddEntryCommand(false));
            AddNiceCommand = new Command(async () => await ExecuteAddEntryCommand(true));

            MessagingCenter.Subscribe<PhonebookEntryViewModel, PhoneBookEntry>(this, "EditEntry", async (obj, item) =>
            {
                LoadItemsCommand.Execute(null);
            });
        }

        async Task ExecuteAddEntryCommand(bool isNice)
        {
            await Navigation.PushModalAsync(new PhonebookEntryPage(new PhoneBookEntry() { 
                Id = Guid.NewGuid().ToString(),
                Kind = isNice ? 1 : 0
            }));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var token = await SecureStorage.GetAsync("token");

                Entries.Clear();
                var items = await PhonebookService.GetEntries(token);
                foreach (var item in items)
                {
                    Debug.WriteLine(item.FirstName);
                    Entries.Add(item);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
