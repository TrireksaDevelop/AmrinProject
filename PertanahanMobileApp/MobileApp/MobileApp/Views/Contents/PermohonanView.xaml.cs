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
			vm = new PermohonanViewModel(stepBar);
			BindingContext = vm;
			stepBar.OnClick += StepBar_OnClick;
		}

		private void StepBar_OnClick(int btn)
		{
			vm.ShowTahapanInfo(btn);
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
		  
			vm.RefreshCommand.Execute(null);
		}
	}

	public class PermohonanViewModel:BaseViewModel
	{
		private permohonan _current;

        private bool showSertifikat;

        public bool ShowSertifikat
        {
            get { return showSertifikat; }
            set { SetProperty(ref showSertifikat , value); }
        }

        public PermohonanViewModel(StepProgressBarControl stepBar)
		{
			this.StepBar = stepBar;
			Permohonans = new ObservableCollection<permohonan>();
			NewCommand = new Command(NewCommandAction);
			MoreCommand = new Command(MoreCommandAction);
			RefreshCommand = new Command(RefreshCommandAction);
            ShowSertifikatCommand = new Command(ShowSertifikatActionAsync);
			RefreshCommand.Execute(null);
		}

        private async void ShowSertifikatActionAsync(object obj)
        {
            if(CurrentItem!=null && CurrentItem.Photo!=null)
            {
                var main = await Helper.GetMainPageAsync();
                if (main != null)
                {
                    await main.Detail.Navigation.PushAsync(new SertifikatView(CurrentItem));
                }

            }else
            {
                Helper.ShowMessageError("Sertifikat Belum Selesai Di Cetak");
            }
        }

        private void RefreshCommandAction(object obj)
		{

			StepBar.Children.Clear();
			LoadAsync();
		}

		private async void MoreCommandAction(object obj)
		{
			var main = await Helper.GetMainPageAsync();
			if (main != null)
			{
			   await main.Detail.Navigation.PushAsync(new  InboxView(CurrentItem.Id));
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
				await Task.Delay(300);
				if (IsBusy)
					return;
				IsBusy = true;

				if(lastPermohonan==null)
				{
					lastPermohonan = await PermohonanService.GetLastPermohonan();
					CurrentItem = lastPermohonan;
				}
			   
				if (CurrentItem != null)
				{
					CurrentItem = await PermohonanService.GetPermohonanById(CurrentItem.Id);
					var layanan = await LayananServices.GetItemAsync(CurrentItem.IdLayanan.ToString());
					if (layanan != null)
					{
						StepBar.Steps = 0;
						StepBar.Children.Clear();
						StepBar.StepSelected = 0;
						StepBar.Steps = layanan.Tahapans.Count();

						NextTahapan = null;
						if (CurrentItem.NextTahapan != null)
							NextTahapan = CurrentItem.NextTahapan;

                        ShowSertifikat = false;
						if (CurrentItem.Tahapans != null && layanan.Tahapans.Count == CurrentItem.Tahapans.Count)
						{
							NextTahapan = new tahapan { Nama = "Tidak Ada", Keterangan = "Proses Telah Selesai" };
                            ShowSertifikat = true;

                            StepBar.Complete();
						}

						if (CurrentItem.CurrentTahapan != null && layanan.Tahapans.Count > CurrentItem.Tahapans.Count)
						{
							var c = layanan.Tahapans.Where(O => O.Id == CurrentItem.CurrentTahapan.Id).FirstOrDefault();
							if (c != null)
							{
								var index = layanan.Tahapans.IndexOf(c);
								StepBar.StepSelected = index + 1;
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
				LastMessage = null;
				if(message!=null && message.Count()>0)
				{
					LastMessage = message.FirstOrDefault();
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

		public StepProgressBarControl StepBar { get; }
		public ObservableCollection<permohonan> Permohonans { get; }
		public Command NewCommand { get; }
		public Command MoreCommand { get; }
		public Command RefreshCommand { get; }
        public Command ShowSertifikatCommand { get; }

        public permohonan CurrentItem { get { return _current; }  set { SetProperty(ref _current, value); } }


		private int _StepSelected;

		public int StepSelected
		{
			get { return _StepSelected; }
			set { SetProperty(ref _StepSelected ,value); }
		}


		private int _steps;
		private inbox _lastMessage;

		public int Steps
		{
			get { return _steps; }
			set {SetProperty(ref _steps ,value); }
		}

		public inbox LastMessage
		{
			get { return _lastMessage; }
			set
			{
			  SetProperty(ref _lastMessage ,value);
			}
		}

		private permohonan _SelectedPermohonan;
		private permohonan lastPermohonan;

		public permohonan SelectedPermohonan
		{
			get { return _SelectedPermohonan; }
			set {SetProperty(ref _SelectedPermohonan ,value);
			   if(value!=null)
				{
					CurrentItem=value;
					RefreshCommand.Execute(null);
				}
			}
		}

		private async Task SetCurrentItem(int id)
		{
			try
			{
				if (IsBusy)
					return;
				IsBusy = true;
				await Task.Delay(300);
				var rest = await PermohonanService.GetPermohonanById(id);
				if (rest != null)
				{
					CurrentItem = rest;
				}
				RefreshCommand.Execute(null);
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



		private tahapan nextTahapan;

		public tahapan NextTahapan
		{
			get { return nextTahapan; }
			set {SetProperty(ref nextTahapan ,value); }
		}

	}
}