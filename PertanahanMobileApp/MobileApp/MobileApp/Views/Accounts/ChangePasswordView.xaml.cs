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
	public partial class ChangePasswordView : ContentPage
	{
		public ChangePasswordView ()
		{
			InitializeComponent ();
		}
	}

    public class ChangePasswordViewModel:BaseViewModel
    {
        public ChangePasswordViewModel()
        {
            SendCommand = new Command(SendCommandAction);
        }

        public Command SendCommand { get; }

        private async void SendCommandAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;


                if(IsValid())
                {
                  var result =await AccountService.ChangePassword(new Models.ChangePasswordModel { Email = Helper.Email, NewPassword = NewPassword });
                    if(result!=null)
                    {
                        
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

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(ConfirmPassword) && NewPassword == ConfirmPassword)
                return true;
            return false;
        }

        private string newPassword;

        public string NewPassword
        {
            get { return newPassword; }
            set { SetProperty(ref newPassword, value); }
        }

        private string confirm;

        public string ConfirmPassword
        {
            get { return confirm; }
            set { SetProperty(ref confirm, value); }
        }
    }
}