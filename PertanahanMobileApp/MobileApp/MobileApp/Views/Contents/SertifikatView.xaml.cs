using MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views.Contents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SertifikatView : ContentPage
    {
        public SertifikatView(Models.permohonan currentItem)
        {
            InitializeComponent();
            BindingContext = new SertifikatViewModel(currentItem.Photo);
        }
    }


    public class SertifikatViewModel : BaseViewModel
    {
        public SertifikatViewModel(byte[] photo)
        {
            Gambar= ImageSource.FromStream(() => new MemoryStream(photo));
        }

        private ImageSource source;

        public ImageSource Gambar
        {
            get {

                return source;
            }
            set
            {
                SetProperty(ref source,  value);
            }
        }
    }
}
