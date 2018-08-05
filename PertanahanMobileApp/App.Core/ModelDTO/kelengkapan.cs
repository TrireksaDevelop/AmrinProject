using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("kelengkapan")]
    public class kelengkapan : BaseNotify
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

        [DbColumn("IdPersyaratan")]
        public int IdPersyaratan
        {
            get { return _idpersyaratan; }
            set
            {
                SetProperty(ref _idpersyaratan, value);
            }
        }

        [DbColumn("IdPermohonan")]
        public int IdPermohonan
        {
            get { return _idpermohonan; }
            set
            {
                SetProperty(ref _idpermohonan, value);
            }
        }

        [DbColumn("Status")]
        public StatusKelengkapan Status
        {
            get { return _status; }
            set
            {
                SetProperty(ref _status, value);
            }
        }

        private int _id;
        private int _idpersyaratan;
        private int _idpermohonan;
        private StatusKelengkapan _status;
    }
}


