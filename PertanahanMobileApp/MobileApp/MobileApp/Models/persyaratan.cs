using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
   public  class persyaratan:PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int IdLayanan
        {
            get { return _idlayanan; }
            set
            {
                SetProperty(ref _idlayanan, value);
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

        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                SetProperty(ref _keterangan, value);
            }
        }


        private int _id;
        private int _idlayanan;
        private string _nama;
        private string _keterangan;
    }
}
