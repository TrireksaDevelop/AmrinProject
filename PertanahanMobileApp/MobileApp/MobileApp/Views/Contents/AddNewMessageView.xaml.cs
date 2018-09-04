using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewMessageView : ContentPage
	{
		public AddNewMessageView (int permohonanId)
		{
			InitializeComponent ();
            BindingContext = new AddnewMessageViewModel(Navigation, permohonanId);
		}
	}

    public class AddnewMessageViewModel:BaseViewModel
    {
        
        public AddnewMessageViewModel(INavigation nav,int Id)
        {
            Navigation = nav;
            Model = new inbox();
            Model.PermohonanId = Id;
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
                if (Valid())
                {
                    Saved = await InboxServices.AddItemAsync(Model);
                    if (Saved)
                    {
                        await Navigation.PopModalAsync();
                        throw new SystemException("Pesan Terkirim");
                    }
                    else
                    {
                        throw new SystemException("Pesan Tidak Terikirm");
                    }
                }
                else
                {
                   throw new SystemException("Input Pesan Anda");
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
            if (Model != null && Model.Message.Length > 0)
                return true;
            return false;
        }

        private inbox model;

        public INavigation Navigation { get; }

        public inbox Model
        {
            get { return model; }
            set { SetProperty(ref model ,value); }
        }


        private bool saved;

        public bool Saved
        {
            get { return saved; }
            set {SetProperty(ref saved ,value); }
        }



    }
}