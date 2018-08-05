using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.ModelDTO
{
    [TableName("tahapanlayanan")]
    public  class tahapanlayanan:BaseNotify
    {
        [PrimaryKey("IdLayayan")]
        [DbColumn("IdLayanan")]
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        [DbColumn("IdTahapan")]
        public int TahapanId
        {
            get { return _tahapan_id; }
            set
            {
                SetProperty(ref _tahapan_id, value);
            }
        }

        [DbColumn("urutan")]
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
