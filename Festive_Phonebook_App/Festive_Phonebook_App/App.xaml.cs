using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Festive_Phonebook_App.Services;
using Festive_Phonebook_App.Views;

namespace Festive_Phonebook_App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new WelcomePage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
