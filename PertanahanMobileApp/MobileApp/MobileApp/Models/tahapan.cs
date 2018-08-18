using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
  public  class tahapan:PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int BidangId
        {
            get { return _bidangid; }
            set
            {
                SetProperty(ref _bidangid, value);
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

        public bidang Bidang { get; set; }
        public int Urutan { get; set; }
        public int LayananId { get; set; }

        private int _id;
        private int _bidangid;
        private string _keterangan;
        private string _nama;
    }
}
