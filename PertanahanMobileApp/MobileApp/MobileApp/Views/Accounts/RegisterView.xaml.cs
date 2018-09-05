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
              if(IsValid())
                {
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

            if (string.IsNullOrEmpty(Model.Alamat)|| string.IsNullOrEmpty(Model.Email)|| string.IsNullOrEmpty(Model.Nama)|| string.IsNullOrEmpty(Model.NIK)
                || string.IsNullOrEmpty(Model.Password)|| string.IsNullOrEmpty(Model.UserName))
                throw new SystemException("Lengkapi Data Anda");

            var chars = @"@.".ToCharArray();
            if (!Model.Email.Contains("@") || !Model.Email.Contains("."))
                throw new SystemException("Periksa Email Anda");

            if(Model.Password.Length < 6)
                throw new SystemException("Password Minimal 6 Character");

            if (!Model.Password.Any(char.IsUpper) || !Model.Password.Any(char.IsNumber)|| !ContainsOnePunctuationMark(Punctuation, Model.Password))
                throw new SystemException("Password Harus Mengandung Huruf Besar, Angka, dan Character Khusus");
            if (Model.Password != Model.UserName)
                throw new SystemException("Password Tidak Sama ");
            return true;
        }


        private  char[] Punctuation = @" !#$%&'()*+,-./:;<=>?@[\]^_`{|}~".ToCharArray();

       public bool ContainsOnePunctuationMark(char[] chars, string text)
        {
            bool found = false;
            foreach (char c in chars)
            {
                if(!found)
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