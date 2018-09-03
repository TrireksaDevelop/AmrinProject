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
	public partial class LayananView : ContentPage
	{
        private LayananViewModel vm;

        public LayananView ()
		{
			InitializeComponent ();
            vm = new LayananViewModel();
            BindingContext = vm;
		}

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var parames = vm.GetKategori();

            var result = await DisplayActionSheet("Kategori","Cancel","", parames.ToArray() );
            vm.Filter(result);
            
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var result = await DisplayActionSheet("Lihat", "Cancel","", "Tahapan", "Persyaratan");
            layanan l = (e.Item as layanan);
            if(result == "Tahapan")
            {
                await Navigation.PushAsync(new Contents.TahapanView(l));
            }else if(result=="Persyaratan")
            {
                await Navigation.PushAsync(new Contents.PersyaratanView(l));
            }
        }
    }

    public class LayananViewModel:BaseViewModel
    {
        private List<string> kategori { get; set; }
        public ObservableCollection<layanan> SourceView { get; set; }
        public Command RefreshCommand { get; }
        public LayananViewModel()
        {
            kategori = new List<string>();
            SourceView = new ObservableCollection<layanan>();
            RefreshCommand = new Command(RefreshAction);
            RefreshCommand.Execute(null);
        }
       
        private async void RefreshAction(object obj)
        {
            await Task.Delay(300);
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                SourceView.Clear();
                var results = await LayananServices.GetItemsAsync();
                var group = results.GroupBy(O => O.Kategori.Nama);
                kategori.Clear();
                kategori.Add("All");
                foreach(var d in group)
                {
                    kategori.Add(d.Key);
                }

                foreach (var item in results)
                {
                    SourceView.Add(item);
                }
            }
          
            finally
            {
                IsBusy = false;
            }
        }

        internal async void Filter(string result)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                await Task.Delay(300);
                SourceView.Clear();

                if (result != "Cancel" && result != "All")
                {
                    var results = await LayananServices.GetItemsAsync();
                    foreach (var item in results.Where(O => O.Kategori.Nama == result))
                    {
                        SourceView.Add(item);
                    }
                }else if(result == "All")
                {
                    RefreshCommand.Execute(null);
                }
            }
          
            finally
            {
                IsBusy = false;
            }
          
        }

        internal List<string> GetKategori()
        {
            return kategori;
        }
    }
}