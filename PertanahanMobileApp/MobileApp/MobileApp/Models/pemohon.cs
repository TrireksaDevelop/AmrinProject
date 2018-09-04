using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp.Models
{
    public class pemohon : PropertyChange
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

        public string NIK
        {
            get { return _nik; }
            set
            {
                SetProperty(ref _nik, value);
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

        public string Alamat
        {
            get { return _alamat; }
            set
            {
                SetProperty(ref _alamat, value);
            }
        }

        private byte[] foto;
       
        public byte[] Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value); }
        }

     

        private int _id;
        private string _userid;
        private string _nik;
        private string _nama;
        private string _alamat;
    }
}


