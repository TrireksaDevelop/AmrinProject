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
	public partial class ForgotPasswordView : ContentPage
	{
		public ForgotPasswordView ()
		{
			InitializeComponent ();
            BindingContext = new ForgotPasswordViewModel(Navigation);
		}
	}


    public class ForgotPasswordViewModel:BaseViewModel
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email ,value); }
        }

        public INavigation Navigation { get; }
        public Command SendCommand { get; }

        public ForgotPasswordViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SendCommand = new Command(SendCommandAction);
        }

        private async void SendCommandAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if (!string.IsNullOrEmpty(Email))
                {
                    var result = await AccountService.ResetPassword(new Models.ChangePasswordModel { Email = Email });
                    result.Email = Email;
                    if(result!=null)
                    {
                        await Navigation.PushModalAsync(new NavigationPage(new ChangePasswordView(result)));
                    }
                }
                else
                {
                    Helper.ShowMessageError("Input User Anda");
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