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
	public partial class ChangeUrl : ContentPage
	{
		public ChangeUrl ()
		{
			InitializeComponent ();
            BindingContext = new ChangeUrlViewModel();
		}
	}

    public class ChangeUrlViewModel : BaseViewModel
    {
        private string server;

        public string Server
        {
            get
            {
                server = Helper.Server;
                return server;
            }
            set
            {
                SetProperty(ref server, value);
                Helper.Server = value;
            }
        }

    }
}