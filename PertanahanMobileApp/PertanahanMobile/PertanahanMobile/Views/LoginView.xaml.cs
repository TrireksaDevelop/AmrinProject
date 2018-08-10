using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PertanahanMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
			InitializeComponent ();
            BindingContext = new LoginViewModel(Navigation);
		}
	}


    public class LoginViewModel : BaseViewModel
    {
        private string _password;
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {

                SetProperty(ref _email, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {

                SetProperty(ref _password, value);
            }
        }

        private INavigation nav;

        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command ForgotCommand { get; }

        public LoginViewModel()
        {

        }
        public LoginViewModel(INavigation navigation)
        {
            this.nav = navigation;
            Email = "aldrich@gmail.com";
            Password = "Sony@77";
            LoginCommand = new Command(LoginAction);
            RegisterCommand = new Command(RegisterAction);
            ForgotCommand = new Command(ForgotAction);
        }

        private  void ForgotAction(object obj)
        {
           // await nav.PushModalAsync(new NavigationPage(new RequestChangePassword()));
        }

        private  void RegisterAction(object obj)
        {
            //await nav.PushModalAsync(new NavigationPage(new Register()));
        }

        private  void LoginAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (Valid())
                {
                //    if (await AccountServices.Login(Email, Password))
                //    {
                //        var app = await Helpers.GetBaseApp();
                //        app.ChangeScreen(new MainPage());
                //    }
                }
                else
                {
                    Helpers.ShowMessageError("Data Tidak Valid");
                }
            }
            catch (Exception ex)
            {
                Helpers.ShowMessageError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool Valid()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                return false;
            return true;
        }
    }
}