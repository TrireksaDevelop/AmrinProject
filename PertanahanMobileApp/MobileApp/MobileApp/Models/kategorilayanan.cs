using System.Collections.Generic;


namespace MobileApp.Models
{
    public class kategorilayanan : PropertyChange
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

        public List<layanan> Layanans { get; set; }

        private int _id;
        private string _nama;
    }
}


