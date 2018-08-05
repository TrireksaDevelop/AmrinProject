using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("tahapan")]
    public class tahapan : BaseNotify
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        [DbColumn("BidangId")]
        public int BidangId
        {
            get { return _bidangid; }
            set
            {
                SetProperty(ref _bidangid, value);
            }
        }


        [DbColumn("Nama")]
        public string Nama
        {
            get { return _nama; }
            set
            {
                SetProperty(ref _nama, value);
            }
        }

        [DbColumn("Keterangan")]
        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                SetProperty(ref _keterangan, value);
            }
        }

        public bidang Bidang { get; set; }
        public int Urutan { get;  set; }
        public int LayananId { get; set; }

        private int _id;
        private int _bidangid;
        private string _keterangan;
        private string _nama;
    }
}


