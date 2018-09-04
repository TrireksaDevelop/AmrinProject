using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Accounts
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
			InitializeComponent ();
			BindingContext = new LoginViewModel(Navigation);
		}

        private async void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new Contents.ChangeUrl());
        }
    }

    public class LoginViewModel:BaseViewModel
	{
		private LoginDto _model;

		public LoginDto Model { get { return _model; } set { SetProperty(ref _model, value); } }

		public LoginViewModel(INavigation navigation)
		{
            Navigation = navigation;
			LoginCommand = new Command(LoginAction);
            ForgotCommand = new Command(ForgotCommandaction);
            RegisterCommand = new Command(RegisterAction);
            Model = new LoginDto() { Email="test3@gmail.com", Password="Sony@77" };
		}

        private async void ForgotCommandaction(object obj)
        {
           await Navigation.PushModalAsync(new ForgotPasswordView());
        }

        private void ChangeUrlCommandAction(object obj)
        {
            throw new NotImplementedException();
        }

        private async void RegisterAction(object obj)
        {
            var app = await Helper.GetBaseApp();
            app.ChangeScreen(new Accounts.RegisterView());
        }

        public INavigation Navigation { get; }
        public Command LoginCommand { get; }
        public Command ForgotCommand { get; }
        public Command RegisterCommand { get; }

        private async void LoginAction(object obj)
		{
			try
			{
				if (IsBusy)
					return;
				IsBusy = true;
				using (var res = new RestServices())
				{
					var result = await res.Post<AuthenticationToken>("Account/Login", Model);
                    if(result!=null && result.roles!=null && result.roles.Where(O=> O.ToUpper()=="PEMOHON").FirstOrDefault()!=null)
                    {
                        var app =await Helper.GetBaseApp();
                        app.SetToken(result);
                        app.ChangeScreen(new MainPage());
                    }
                    else
                    {
                        throw new SystemException("Anda Tidak Memiliki Akses");
                    }
				}
			}
			catch (Exception ex)
			{
				Helper.ShowMessageError(ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private bool Valid()
		{
			var result = true;
			if (string.IsNullOrEmpty(Model.Email) || string.IsNullOrEmpty(Model.Password))
				result = false;
			return result;

				
		}
	}
}