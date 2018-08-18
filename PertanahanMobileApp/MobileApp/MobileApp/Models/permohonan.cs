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

        public List<kelengkapan> Kelengkapans { get; internal set; }
        public List<progress> Tahapans { get; internal set; }
        public List<kelengkapan> Persyaratan { get; internal set; }
        public layanan Layanan { get; internal set; }

        private int _id;
        private int _idpemohon;
        private int _idlayanan;
        private StatusPermohonan _status;
    }
}


