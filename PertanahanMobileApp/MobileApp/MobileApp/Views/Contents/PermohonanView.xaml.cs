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
	public partial class PermohonanView : ContentPage
	{
		public PermohonanView ()
		{
			InitializeComponent ();
            BindingContext = new PermohonanViewModel();
		}
	}

    public class PermohonanViewModel:BaseViewModel
    {
        public PermohonanViewModel()
        {
            Permohonans = new ObservableCollection<permohonan>();
            Permohonans.Add(new permohonan { Layanan = new layanan { Nama = "Apa" }, Id = 1 });
        }
        public ObservableCollection<permohonan> Permohonans { get; }
    }
}