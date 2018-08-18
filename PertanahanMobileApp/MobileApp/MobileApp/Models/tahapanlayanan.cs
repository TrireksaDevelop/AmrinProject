using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public  class tahapanlayanan:PropertyChange
    {
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public int TahapanId
        {
            get { return _tahapan_id; }
            set
            {
                SetProperty(ref _tahapan_id, value);
            }
        }

        public int Urutan
        {
            get { return _urutan; }
            set
            {
                SetProperty(ref _urutan, value);
            }
        }

        private int _id;
        private int _tahapan_id;
        private int _urutan;
    }
}
