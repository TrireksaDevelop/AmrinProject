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
		public InboxView (int id)
		{
			InitializeComponent ();
            BindingContext = new InboxViewModel(id);
		}

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var vm = this.BindingContext as InboxViewModel;
            vm.ReplayCommand.Execute(null);
        }
    }

    public class InboxViewModel:BaseViewModel
    {
        public int PermohonanId { get; }
        public ObservableCollection<inbox> SourceView { get; set; }
        public Command ReplayCommand { get; }
        public Command RefreshCommand { get; }

        public InboxViewModel(int id)
        {
            PermohonanId = id;
            SourceView = new ObservableCollection<inbox>();
            ReplayCommand = new Command(ReplayCommandAction);
            RefreshCommand = new Command(RefreshAction);

            RefreshCommand.Execute(null);
        }

        private async void ReplayCommandAction(object obj)
        {
            var main = await Helper.GetMainPageAsync();
              await main.Detail.Navigation.PushModalAsync(new AddNewMessageView(PermohonanId));
        }

        private async void RefreshAction(object obj)
        {
            try
            {
                await Task.Delay(300);
              
                IsBusy = true;
                SourceView.Clear();
                var results = await InboxServices.GetItemsAsync(PermohonanId);
                foreach (var item in results)
                {
                    SourceView.Add(item);
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
    }
}