using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PermohonanHome : ContentPage
	{
		public PermohonanHome ()
		{
			InitializeComponent ();
            BindingContext = new PermohonanHomeViewModel();
		}
	}



    public class PermohonanHomeViewModel:BaseViewModel
    {
        public PermohonanHomeViewModel()
        {
            LoadAsync();
        }

        private async void LoadAsync()
        {
            await Task.Delay(200);
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                var main = await Helper.GetMainPageAsync();
                if(main!=null)
                {
                    Type page = null;
                    if(await IsNewPermohonan())
                    {
                        page = typeof(AddNewPermohonanView);
                    }
                    else
                    {
                        page = typeof(PermohonanView);
                    }

                    main.Detail = new NavigationPage((Page)Activator.CreateInstance(page));
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


        public async Task<bool> IsNewPermohonan()
        {
            try
            {
                var result = await PermohonanService.GetLastPermohonan();
                if (result == null)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
                return true;
            }
        }
    }
}