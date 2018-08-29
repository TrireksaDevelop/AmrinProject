﻿using MobileApp.ViewModels;
using MobileApp.Views.Contents;
using System;
using System.Collections.Generic;
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
                Title = "Inbox",
                Icon = "ic_move_to_inbox_white_48pt.png",
                TargetType = typeof(InboxView)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Permohonan",
                Icon = "ic_send_white.png",
                TargetType = typeof(PermohonanView)
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
                TargetType = typeof(Home)
            });
            menuList.Add(new MasterPageItem()
            {
                Title = "Logout",
                Icon = "ic_lock_white_48dp.png",
                TargetType = typeof(Home)
            });


            // Setting our list to be ItemSource for ListView in MainPage.xaml  
            navigationDrawerList.ItemsSource = menuList;
            // Initial navigation, this can be used for our home page  
            //	Detail = (Page)Activator.CreateInstance(typeof(Home));
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Home)));
        }

		private async void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

			var item = (MasterPageItem)e.SelectedItem;
			Type page = item.TargetType;
            if(item.Title=="Permohonan")
            {
                if(await vm.IsNewPermohonan())
                {
                    page = typeof(AddNewPermohonanView);
                }
            }


		    Detail = new NavigationPage((Page)Activator.CreateInstance(page));
		 //  Detail = (Page)Activator.CreateInstance(page);
			IsPresented = false;
		}

		private void profileGesture_Clicked(object sender, EventArgs e)
		{

		}

        public void SetPage(Type page)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
        }


	}


    public class MainPageViewModel:BaseViewModel
    {
        public async Task<bool> IsNewPermohonan()
        {
            var result = await PermohonanService.GetLastPermohonan();
            if (result == null)
                return true;
            else
                return false;

        }
    }
}