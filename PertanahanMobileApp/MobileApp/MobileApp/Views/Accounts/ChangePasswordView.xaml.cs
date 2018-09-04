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
	public partial class ChangePasswordView : ContentPage
	{
		public ChangePasswordView (Models.ChangePasswordModel result)
		{
			InitializeComponent ();
            BindingContext = new ChangePasswordViewModel(result, Navigation);
		}
	}

    public class ChangePasswordViewModel:BaseViewModel
    {
        public ChangePasswordViewModel(Models.ChangePasswordModel result, INavigation navigation)
        {
            Model = result;
            Navigation = navigation;
            SendCommand = new Command(SendCommandAction);
        }

        public ChangePasswordModel Model { get; }
        public INavigation Navigation { get; }
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
                    Model.NewPassword = NewPassword;
                  var result =await AccountService.ChangePassword(Model);
                    if(result!=null)
                    {
                        Helper.ShowMessage("Password Berhasil Dibuah");
                       await Navigation.PopToRootAsync();
                    }
                    throw new SystemException("Gagal Diubah");
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
            if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(ConfirmPassword) && NewPassword == ConfirmPassword)
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