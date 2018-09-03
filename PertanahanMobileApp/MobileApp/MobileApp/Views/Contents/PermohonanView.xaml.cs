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
        private PermohonanViewModel vm;

        public PermohonanView ()
		{
			InitializeComponent ();
            vm = new PermohonanViewModel();
            BindingContext = vm;
            stepBar.OnClick += StepBar_OnClick;
        }

        private void StepBar_OnClick(int btn)
        {
            vm.ShowTahapanInfo(btn);
        }
    }

    public class PermohonanViewModel:BaseViewModel
    {
        private permohonan _current;

        public PermohonanViewModel()
        {
            Permohonans = new ObservableCollection<permohonan>();
            NewCommand = new Command(NewCommandAction);
            MoreCommand = new Command(MoreCommandAction);

            LoadAsync();
        }

        private async void MoreCommandAction(object obj)
        {
            var main = await Helper.GetMainPageAsync();
            if (main != null)
            {
               await main.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(typeof(InboxView)));
                main.Detail.BindingContext = new InboxViewModel(CurrentItem.Id);
            }
        }

        private async void NewCommandAction(object obj)
        {
            var main = await Helper.GetMainPageAsync();
            if(main!=null)
            {
                main.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(AddNewPermohonanView)));
            }
        }

        private async void LoadAsync()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                CurrentItem = await PermohonanService.GetLastPermohonan();
                if (CurrentItem != null)
                {
                    var layanan = await LayananServices.GetItemAsync(CurrentItem.IdLayanan.ToString());
                    if (layanan != null)
                    {
                        Steps = layanan.Tahapans.Count();
                        if (CurrentItem.CurrentTahapan != null)
                        {
                            var c = layanan.Tahapans.Where(O => O.Id == CurrentItem.CurrentTahapan.Id).FirstOrDefault();
                            if (c != null)
                            {
                                var index = layanan.Tahapans.IndexOf(c);
                                StepSelected = index + 1;
                            }
                        }
                    }
                }
                var result = await PermohonanService.GetPermohonans();
                Permohonans.Clear();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        Permohonans.Add(item);
                    }
                }


                var message = await InboxServices.GetItemsAsync(CurrentItem.Id);
                if(message!=null)
                {
                    LastMessage = message.Last();
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

        internal async void ShowTahapanInfo(int pos)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                var layanan = await LayananServices.GetItemAsync(CurrentItem.IdLayanan.ToString());
                if (layanan != null)
                {
                    var c = layanan.Tahapans[pos - 1];
                    if (c != null)
                    {
                        Helper.ShowMessage(c.Nama);
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

        public ObservableCollection<permohonan> Permohonans { get; }
        public Command NewCommand { get; }
        public Command MoreCommand { get; }

        public permohonan CurrentItem { get { return _current; }  set { SetProperty(ref _current, value); } }


        private int _StepSelected;

        public int StepSelected
        {
            get { return _StepSelected; }
            set { SetProperty(ref _StepSelected ,value); }
        }


        private int _steps;
        private InboxItem _lastMessage;

        public int Steps
        {
            get { return _steps; }
            set {SetProperty(ref _steps ,value); }
        }

        public InboxItem LastMessage
        {
            get { return _lastMessage; }
            set
            {
              SetProperty(ref _lastMessage ,value);
            }
        }
    }
}