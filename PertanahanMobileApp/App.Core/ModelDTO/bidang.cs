using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("bidang")]
    public class bidang : BaseNotify
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

        [DbColumn("Deskripsi")]
        public string Descripsi
        {
            get { return _deskripsi; }
            set
            {
                SetProperty( ref _deskripsi, value);
            }
        }


        [DbColumn("PetugasId")]
        public int PetugasId
        {
            get { return _petugasId; }
            set
            {
                SetProperty(ref _petugasId, value);
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

        public petugas Petugas { get; set; }
        public IEnumerable<tahapan> Tahapans { get;  set; }

        private int _id;
        private string _nama;
        private int _petugasId;
        private string _deskripsi;
    }
}


