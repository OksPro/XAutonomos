using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAutonomos.Views;

namespace XAutonomos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NavigationPage main = new NavigationPage(new LoginPage());
            MainPage = main;
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
