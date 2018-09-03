using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class permohonan : PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int IdPemohon
        {
            get { return _idpemohon; }
            set
            {
                SetProperty(ref _idpemohon, value);
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

        public StatusPermohonan Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        public List<kelengkapan> Kelengkapans { get; set; }
        public List<progress> Tahapans { get; set; }
        public List<kelengkapan> Persyaratan { get; set; }
        public layanan Layanan { get; set; }
        public tahapan CurrentTahapan { get; set; }
        public tahapan NextTahapan { get; set; }

        private int _id;
        private int _idpemohon;
        private int _idlayanan;
        private StatusPermohonan _status;
    }
}


