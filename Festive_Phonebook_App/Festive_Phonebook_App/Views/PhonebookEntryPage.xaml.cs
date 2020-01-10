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
    public partial class PhonebookEntryPage : ContentPage
    {
        public PhonebookEntryPage(PhoneBookEntry entry)
        {
            InitializeComponent();
            BindingContext = new PhonebookEntryViewModel(entry, Navigation);
        }

        public PhonebookEntryPage()
        {
            InitializeComponent();
        }
    }
}