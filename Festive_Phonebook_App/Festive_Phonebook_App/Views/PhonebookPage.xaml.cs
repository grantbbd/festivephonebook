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
    }
}