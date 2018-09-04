using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("petugas")]
    public class petugas : BaseNotify
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

        [DbColumn("Nama")]
        public string Nama
        {
            get { return _nama; }
            set
            {
                SetProperty(ref _nama, value);
            }
        }

        [DbColumn("NIP")]
        public string NIP
        {
            get { return _nip; }
            set
            {
                SetProperty(ref _nip, value);
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

        [DbColumn("Jabatan")]
        public string Jabatan
        {
            get { return _jabatan; }
            set
            {
                SetProperty(ref _jabatan, value);
            }
        }

        private byte[] foto;
        [DbColumn("Foto")]
        public byte[] Foto
        {
            get { return foto; }
            set { SetProperty(ref foto ,value); }
        }


        public string Email { get; set; }
        public IEnumerable<bidang> Bidangs { get;  set; }

        private int _id;
        private string _userid;
        private string _nama;
        private string _nip;
        private string _alamat;
        private string _jabatan;
    }
}


