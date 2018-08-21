using MobileApp.Models;
using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersyaratanView : ContentPage
	{
        public PersyaratanView()
        {
            InitializeComponent();
        
        }
		public PersyaratanView (Models.layanan l)
		{
			InitializeComponent ();
            BindingContext = new PersyaratanViewModel(l);
		}


	}



    public class PersyaratanViewModel : BaseViewModel
    {
        public ObservableCollection<persyaratan> SourceView { get; set; }

        public PersyaratanViewModel(layanan item)
        {
            Title = item.Nama.ToUpper();
            SourceView = new ObservableCollection<persyaratan>(item.Persyaratans);
        }
    }
}