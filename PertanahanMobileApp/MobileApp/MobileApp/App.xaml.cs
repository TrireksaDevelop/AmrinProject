﻿using System;
using Xamarin.Forms;
using MobileApp.Views;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MobileApp
{
	public partial class App : Application
	{
        public AuthenticationToken Token { get;  set; }

        public App ()
		{
			InitializeComponent();
            MessagingCenter.Subscribe<MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

            });
            ChangeScreen(new Views.Accounts.LoginView());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        

        public void ChangeScreen(Page page)
        {
            Current.MainPage =page;
        }

        internal void SetToken(AuthenticationToken token)
        {
            this.Token = token;
        }

        internal Task<AuthenticationToken> GetToken()
        {
            return Task.FromResult( Token);
        }

    }
}
