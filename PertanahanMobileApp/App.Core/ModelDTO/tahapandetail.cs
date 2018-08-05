using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.ModelDTO
{
    [TableName("tahapandetail")]
    public class tahapandetail : BaseNotify
    {
        [DbColumn("BidangId")]
        public int BidangId
        {
            get { return _idbidang; }
            set
            {
                SetProperty(ref _idbidang, value);
            }
        }

        [DbColumn("TahapanId")]
        public int TahapanId
        {
            get { return _idtahapan; }
            set
            {
                SetProperty(ref _idtahapan, value);
            }
        }

        private int _id;
        private int _idbidang;
        private int _idtahapan;
    }
}
