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
	public partial class RegisterView : ContentPage
	{
		public RegisterView ()
		{
			InitializeComponent ();
            this.BindingContext = new RegisterViewModel();
		}
	}


    public class RegisterViewModel:BaseViewModel
    {
        public UserRegister Model { get; set; }
        public RegisterViewModel()
        {
            Model = new UserRegister();
            RegisterCommand = new Command(RegisterAction);
            LoginCommand = new Command(LoginAction);
        }

        private async void LoginAction(object obj)
        {
            var app = await Helper.GetBaseApp();
            app.ChangeScreen(new Accounts.LoginView());
        }

        public Command RegisterCommand { get; }
        public Command LoginCommand { get; }

        private async void RegisterAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                var registered = await AccountService.Register(Model);
                if (registered)
                {
                    Helper.ShowMessage("Registrasi Berhasil, Silahkan Login");
                }
                else
                {
                    Helper.ShowMessageError("Registrasi Tidak berhasil");
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
    }
}