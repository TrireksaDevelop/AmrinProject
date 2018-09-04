using MobileApp.Models;
using MobileApp.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Accounts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentPage
    {
        private ProfileViewModel vm;

        public ProfileView(Models.pemohon pemohon)
        {
            InitializeComponent();
          vm= new ProfileViewModel(pemohon);
            BindingContext = vm;
        }

        private async void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            try
            {
                var action = await DisplayActionSheet("Pilih", "Cancel", null, "File", "Camera");
                if (action == "Camera")
                {
                    var task = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        PhotoSize = PhotoSize.Custom,
                        CustomPhotoSize = 70,
                        CompressionQuality = 60
                    });
                    vm.Photo = await CompleteTakePhoto(task);

                }
                else if (action == "File")
                {
                    var task = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Custom, CustomPhotoSize = 70, CompressionQuality=60 });
                    vm.Photo = await CompleteTakePhoto(task);
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
            }
        }

        private async Task<ImageSource> CompleteTakePhoto(MediaFile task)
        {
            await Task.Delay(500);
            var imageS = ImageSource.FromStream(() =>
            {
                var stream = task.GetStream();
                return stream;
            });
            using (var memoryStream = new MemoryStream())
            {

                task.GetStream().CopyTo(memoryStream);
                vm.Profile.Foto = memoryStream.ToArray();

            }
            return imageS;
        }
    }

    public class ProfileViewModel : BaseViewModel
    {

        private ImageSource photo;

        public ImageSource Photo
        {
            get
            {
                if (photo == null)
                {
                    photo = ImageSource.FromFile("user.png");
                }
                return photo;
            }
            set { SetProperty(ref photo, value); }
        }



        private pemohon profile;

        public pemohon Profile
        {
            get { return profile; }
            set { SetProperty(ref profile ,value); }
        }

        public Command SaveCommand { get; }

        public ProfileViewModel(pemohon pemohon)
        {
            this.Profile = pemohon;
            SaveCommand = new Command(SaveCommandAction);

        }

        private async void SaveCommandAction(object obj)
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                AccountService.Pemohon = Profile;
                if (await AccountService.SaveProfileProfile())
                {
                    Helper.ShowMessageError("Data Tersimpan");
                }else
                {
                    throw new SystemException("Data Tidak Tersimpan");
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