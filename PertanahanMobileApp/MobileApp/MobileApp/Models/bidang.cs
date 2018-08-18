using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class bidang : PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Descripsi
        {
            get { return _deskripsi; }
            set
            {
                SetProperty( ref _deskripsi, value);
            }
        }


        public int PetugasId
        {
            get { return _petugasId; }
            set
            {
                SetProperty(ref _petugasId, value);
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

        public petugas Petugas { get; set; }
        public IEnumerable<tahapan> Tahapans { get;  set; }

        private int _id;
        private string _nama;
        private int _petugasId;
        private string _deskripsi;
    }
}


