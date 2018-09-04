using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class petugas : PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string UserId
        {
            get { return _userid; }
            set
            {
                SetProperty(ref _userid, value);
            }
        }

        public string Nama
        {
            get { return _nama; }
            set
            {
                SetProperty(ref _nama, value);
            }
        }

        public string NIP
        {
            get { return _nip; }
            set
            {
                SetProperty(ref _nip, value);
            }
        }

        public string Alamat
        {
            get { return _alamat; }
            set
            {
                SetProperty(ref _alamat, value);
            }
        }

        public string Jabatan
        {
            get { return _jabatan; }
            set
            {
                SetProperty(ref _jabatan, value);
            }
        }


        private byte[] foto;
        public byte[] Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value); }
        }


        private ImageSource photo;

        public ImageSource Photo
        {
            get {
                if(Foto!=null && Foto.Length>0)
                    photo= ImageSource.FromStream(() => new MemoryStream(Foto));
                return photo;
            }
            set { SetProperty(ref photo ,value); }
        }



        public string Email { get; set; }
        public IEnumerable<bidang> Bidangs { get;  set; }

        private int _id;
        private string _userid;
        private string _nama;
        private string _nip;
        private string _alamat;
        private string _jabatan;
    }
}


