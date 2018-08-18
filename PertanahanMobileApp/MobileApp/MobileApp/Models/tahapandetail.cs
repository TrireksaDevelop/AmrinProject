using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class tahapandetail : PropertyChange
    {
        public int BidangId
        {
            get { return _idbidang; }
            set
            {
                SetProperty(ref _idbidang, value);
            }
        }

        public int TahapanId
        {
            get { return _idtahapan; }
            set
            {
                SetProperty(ref _idtahapan, value);
            }
        }

        private int _idbidang;
        private int _idtahapan;
    }
}
