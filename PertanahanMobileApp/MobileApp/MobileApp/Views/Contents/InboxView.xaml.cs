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
	public partial class InboxView : ContentPage
	{
		public InboxView ()
		{
			InitializeComponent ();
		}
	}

    public class InboxViewModel:BaseViewModel
    {
        public int PermohonanId { get; }
        public ObservableCollection<InboxItem> SourceView { get; set; }
        public Command RefreshCommand { get; }

        public InboxViewModel(int id)
        {
            PermohonanId = id;
            SourceView = new ObservableCollection<InboxItem>();
            RefreshCommand = new Command(RefreshAction);
            RefreshCommand.Execute(null);
        }

        private async void RefreshAction(object obj)
        {
            await Task.Delay(300);
            SourceView.Clear();
            var results = await InboxServices.GetItemsAsync(PermohonanId);
            foreach(var item in results)
            {
                SourceView.Add(item);
            }
        }
    }
}