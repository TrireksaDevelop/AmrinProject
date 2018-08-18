using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class progress : PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
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

        public int IdTahapan
        {
            get { return _idtahapan; }
            set
            {
                SetProperty(ref _idtahapan, value);
            }
        }

        private int _id;
        private int _idpermohonan;
        private int _idtahapan;
    }
}


