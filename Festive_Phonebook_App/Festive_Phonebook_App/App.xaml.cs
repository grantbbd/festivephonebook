using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Festive_Phonebook_App.Services;
using Festive_Phonebook_App.Views;
using Flurl.Http;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Festive_Phonebook_App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<PhonebookServiceImpl>();

         //   FlurlHttp.ConfigureClient("https://0.0.0.0:8899", cli =>
         //       cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());

            _ = Init();
        }

        private async Task Init()
        {
            string token = await SecureStorage.GetAsync("token");

            //if (string.IsNullOrWhiteSpace(token))
            //{
                MainPage = new WelcomePage();
            //} else
            //{
             //   MainPage = new PhonebookPage();
            //}
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
