using MobileApp.Models;
using MobileApp.ViewModels;
using MobileApp.Views.Contents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
        private MainPageViewModel vm;
        private List<MasterPageItem> menuList;
		public MainPage()
		{
			InitializeComponent();
            vm = new MainPageViewModel();
            BindingContext = vm;
			menuList = new List<MasterPageItem>();
			// Adding menu items to menuList and you can define title ,page and icon  
			menuList.Add(new MasterPageItem()
			{
				Title = "Home",
				Icon = "home1.png",
				TargetType = typeof(Home)
			});
        
            menuList.Add(new MasterPageItem()
            {
                Title = "Permohonan",
                Icon = "ic_send_white.png",
                TargetType = typeof(PermohonanHome)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Info Layanan",
                Icon = "ic_info_white_48pt.png",
                TargetType = typeof(LayananView)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "About",
                Icon = "ic_help_white_48dp.png",
                TargetType = typeof(AboutPage)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Logout",
                Icon = "ic_lock_white_48dp.png",
            });


            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page  
            //	Detail = (Page)Activator.CreateInstance(typeof(Home));
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Home)));




        }

		private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

			var item = (MasterPageItem)e.SelectedItem;
			Type page = item.TargetType;
            if (item.Title=="Logout")
            {
                vm.Logout();
            }
            else
            {

                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                //  Detail = (Page)Activator.CreateInstance(page);
                IsPresented = false;
            }


		}

		private void profileGesture_Clicked(object sender, EventArgs e)
		{
            Detail = new NavigationPage(new Accounts.ProfileView(vm.Pemohon));
        }

        public void SetPage(Type page)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
        }
	}


    public class MainPageViewModel:BaseViewModel
    {

        private ImageSource photo;

       public  ImageSource Photo
        {
            get
            {
                if(photo==null)
                {
                    photo = ImageSource.FromFile("user.png");
                }
                return photo;
            }
            set { SetProperty(ref photo, value); }
        }

        private pemohon _pemohon;

        public pemohon Pemohon { get => _pemohon; set => SetProperty(ref _pemohon, value); }
        public Command ProfileCommand { get; }

        public MainPageViewModel()
        {
            ProfileCommand = new Command(ProfileCommandAction);
           Photo = ImageSource.FromFile("user.png"); 
            LoadAsync();
        }

        private async void ProfileCommandAction(object obj)
        {
            var main = await Helper.GetMainPageAsync();
            main.Detail = new NavigationPage(new Accounts.ProfileView(Pemohon));
        }

        private async void LoadAsync()
        {
            try
            {
                await Task.Delay(300);
                if (IsBusy)
                    return;
                IsBusy = true;
                Pemohon = await AccountService.GetProfile();
                if (Pemohon != null && Pemohon.Foto != null && Pemohon.Foto.Length > 0)
                    Photo = ImageSource.FromStream(() => new MemoryStream(Pemohon.Foto));
                else
                    Photo = ImageSource.FromFile("user.png");
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                Pemohon = new pemohon();
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal async void Logout()
        {
            var app = await Helper.GetBaseApp();
            if(app!=null)
            {
                app.ChangeScreen(new Views.Accounts.LoginView());
            }
        }
    }
}