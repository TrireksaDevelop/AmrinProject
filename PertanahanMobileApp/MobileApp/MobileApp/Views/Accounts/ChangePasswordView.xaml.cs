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
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
                throw new SystemException("Lengkapi Data");
            if (Model.NewPassword.Length< 6)
                throw new SystemException("Password Minimal 6 Character");
            if (!Model.NewPassword.Any(char.IsUpper) || !Model.NewPassword.Any(char.IsNumber) || !ContainsOnePunctuationMark(Punctuation, Model.NewPassword))
                throw new SystemException("Password Harus Mengandung Huruf Besar, Angka, dan Character Khusus");
            if (Model.NewPassword != Model.OldPassword)
                throw new SystemException("Password Tidak Sama ");
            return true;
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

        private char[] Punctuation = @" !#$%&'()*+,-./:;<=>?@[\]^_`{|}~".ToCharArray();

        public bool ContainsOnePunctuationMark(char[] chars, string text)
        {
            bool found = false;
            foreach (char c in chars)
            {
                if (!found)
                {
                    if (text.Contains(c))
                    {
                        found = true;
                    }
                }
            }

            return found;
        }

    }
}