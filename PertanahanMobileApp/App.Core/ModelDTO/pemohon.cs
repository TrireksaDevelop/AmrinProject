using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("pemohon")]
    public class pemohon : BaseNotify
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

        [DbColumn("UserId")]
        public string UserId
        {
            get { return _userid; }
            set
            {
                SetProperty(ref _userid, value);
            }
        }

        [DbColumn("NIK")]
        public string NIK
        {
            get { return _nik; }
            set
            {
                SetProperty(ref _nik, value);
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

        [DbColumn("Alamat")]
        public string Alamat
        {
            get { return _alamat; }
            set
            {
                SetProperty(ref _alamat, value);
            }
        }

        private int _id;
        private string _userid;
        private string _nik;
        private string _nama;
        private string _alamat;
    }
}


