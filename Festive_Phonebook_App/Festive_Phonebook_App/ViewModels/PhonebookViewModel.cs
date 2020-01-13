using Festive_Phonebook_App.Models;
using Festive_Phonebook_App.Services;
using Festive_Phonebook_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        public ICommand LogoffCommand { get; set; }
        public ICommand SearchEntriesCommand { get; set; }
        public bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value);  }
        }

        public PhonebookViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Entries = new ObservableCollection<PhoneBookEntry>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(null);

            AddNaughtyCommand = new Command(async () => await ExecuteAddEntryCommand(false));
            AddNiceCommand = new Command(async () => await ExecuteAddEntryCommand(true));
            LogoffCommand = new Command(() => ExecuteLogoffCommand());

            MessagingCenter.Subscribe<PhonebookEntryViewModel, PhoneBookEntry>(this, "EditEntry", async (obj, item) =>
            {
                LoadItemsCommand.Execute(null);
            });

            SearchEntriesCommand = new Command<string>((string query) => {
                FilterList(query);
            });

        }

        public void FilterList(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                LoadItemsCommand.Execute(null);
                return;
            }
            else
            {
                Entries = new ObservableCollection<PhoneBookEntry>(
                    Entries.Where(x => x.FirstName.ToLowerInvariant().Contains(query.ToLowerInvariant())
                    || x.Surname.ToLowerInvariant().Contains(query.ToLowerInvariant())));
                OnPropertyChanged(nameof(Entries));
            }
        }

        async Task ExecuteAddEntryCommand(bool isNice)
        {
            await Navigation.PushModalAsync(new PhonebookEntryPage(new PhoneBookEntry() { 
                Id = Guid.Empty.ToString(),
                Kind = isNice ? 1 : 2
            }));
        }

        void ExecuteLogoffCommand()
        {
            SecureStorage.Remove("token");
            App.Current.MainPage = new WelcomePage();
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
                IsRefreshing = false;
            }
        }

    }
}
