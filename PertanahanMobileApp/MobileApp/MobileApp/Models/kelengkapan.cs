using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class kelengkapan : PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int IdPersyaratan
        {
            get { return _idpersyaratan; }
            set
            {
                SetProperty(ref _idpersyaratan, value);
            }
        }

        public int IdPermohonan
        {
            get { return _idpermohonan; }
            set
            {
                SetProperty(ref _idpermohonan, value);
            }
        }

        public StatusKelengkapan Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        private int _id;
        private int _idpersyaratan;
        private int _idpermohonan;
        private StatusKelengkapan _status;
    }
}


