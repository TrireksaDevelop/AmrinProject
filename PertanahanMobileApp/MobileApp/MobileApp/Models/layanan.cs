using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class layanan : PropertyChange
    {
     
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
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

        public int IdKategoriLayanan
        {
            get { return _kategorilayanan_id; }
            set
            {
                SetProperty(ref _kategorilayanan_id, value);
            }
        }

        public kategorilayanan Kategori { get; set; }
        public List<tahapan> Tahapans { get; set; }
        public List<persyaratan> Persyaratans { get; set; }
        public string Keterangan { get; internal set; }

        private int _id;
        private string _nama;
        private int _kategorilayanan_id;
    }
}
