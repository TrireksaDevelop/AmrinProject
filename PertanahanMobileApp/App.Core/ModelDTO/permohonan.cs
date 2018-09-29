using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("permohonan")]
    public class permohonan : BaseNotify
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

        [DbColumn("IdPemohon")]
        public int IdPemohon
        {
            get { return _idpemohon; }
            set
            {
                SetProperty(ref _idpemohon, value);
            }
        }

        [DbColumn("IdLayanan")]
        public int IdLayanan
        {
            get { return _idlayanan; }
            set
            {
                SetProperty(ref _idlayanan, value);
            }
        }

        [DbColumn("Status")]
        public StatusPermohonan Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        private byte[] photo;
        [DbColumn("Photo")]
        public byte[] Photo
        {
            get { return photo; }
            set { SetProperty(ref photo ,value); }
        }


        public List<kelengkapan> Kelengkapans { get;  set; }
        public List<progress> Tahapans { get;  set; }
        public layanan Layanan { get;  set; }
        public tahapan CurrentTahapan { get; set; }
        public tahapan NextTahapan { get; set; }
        public pemohon Pemohon { get; set; }

        private int _id;
        private int _idpemohon;
        private int _idlayanan;
        private StatusPermohonan _status;
    }
}


