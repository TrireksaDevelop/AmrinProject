using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("persyaratan")]
    public class persyaratan : BaseNotify
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

        [DbColumn("IdLayanan")]
        public int IdLayanan
        {
            get { return _idlayanan; }
            set
            {
                SetProperty(ref _idlayanan, value);
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

        [DbColumn("keterangan")]
        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                SetProperty(ref _keterangan, value);
            }
        }


        private int _id;
        private int _idlayanan;
        private string _nama;
        private string _keterangan;
    }
}


