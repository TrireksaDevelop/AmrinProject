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
	public partial class AddNewPermohonanView : ContentPage
	{
		public AddNewPermohonanView ()
		{
			InitializeComponent ();
            BindingContext = new AddNewPermohonanViewModel();
		}
	}

    public class AddNewPermohonanViewModel:BaseViewModel
    {
        public List<string> Kategories { get; set; }
        public ObservableCollection<layanan> SourceView { get; set; }
        public Command RefreshCommand { get; }
        public Command DaftarCommand { get; }
        public AddNewPermohonanViewModel()
        {
            Kategories = new List<string>();
            SourceView = new ObservableCollection<layanan>();
            DaftarCommand = new Command(DaftarAction);
            RefreshCommand = new Command(RefreshAction);
            RefreshCommand.Execute(null);
        }

        private async void DaftarAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                if(Valid())
                {
                    var item = new permohonan { IdLayanan = SelectedItem.Id };
                    bool isCreated = await PermohonanService.CreateNewPermohonan(item);
                    if(isCreated)
                    {
                        var main = await Helper.GetMainPageAsync();
                        main.SetPage(typeof(PermohonanView));
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

        private bool Valid()
        {
            if(SelectedCategory==null || SelectedItem==null || string.IsNullOrEmpty(Keterangan))
            {
                return false;
            }
            return true;
        }

        private async void RefreshAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
           
                SourceView.Clear();
                var results = await LayananServices.GetItemsAsync();
                var group = results.GroupBy(O => O.Kategori.Nama);
                Kategories.Clear();
                foreach (var d in group)
                {
                    Kategories.Add(d.Key);
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

        internal async void Filter(string result)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                await Task.Delay(300);
                SourceView.Clear();
                var results = await LayananServices.GetItemsAsync();
                foreach (var item in results.Where(O => O.Kategori.Nama== result))
                {
                    SourceView.Add(item);
                }
            }

            finally
            {
                IsBusy = false;
            }

        }


        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set { SetProperty(ref selectedCategory, value);
                if (value != null)
                    Filter(value);

            }
        }


        private layanan lay;

        public layanan SelectedItem
        {
            get { return lay; }
            set {SetProperty(ref lay , value); }
        }


        private string ket;

        public string Keterangan
        {
            get { return ket; }
            set {SetProperty(ref ket ,value); }
        }





    }
}