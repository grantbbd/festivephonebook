using Festive_Phonebook_App.Models;
using Festive_Phonebook_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Festive_Phonebook_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhonebookPage : ContentPage
    {
        public PhonebookPage()
        {
            InitializeComponent();
            BindingContext = new PhonebookViewModel(Navigation);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as PhoneBookEntry;
            if (item == null)
                return;

            await Navigation.PushModalAsync(new PhonebookEntryPage(item));

            ItemsListView.SelectedItem = null;
        }

        private void SearchEntriesBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((PhonebookViewModel)BindingContext).FilterList(((SearchBar)sender).Text);
        }
    }
}