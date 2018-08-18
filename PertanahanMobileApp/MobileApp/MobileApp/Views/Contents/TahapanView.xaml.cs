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
	public partial class TahapanView : ContentPage
	{
        public TahapanView()
        {
            InitializeComponent();
        }
		public TahapanView (layanan item)
		{
			InitializeComponent ();
            BindingContext = new TahapanViewModel(item);
		}
	}



    public class TahapanViewModel :BaseViewModel
    {
       
        public ObservableCollection<tahapan> SourceView { get; set; }
        public TahapanViewModel(layanan item)
        {
            Title = item.Nama.ToUpper();
            SourceView = new ObservableCollection<tahapan>(item.Tahapans);
        }

    }
}