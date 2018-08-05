using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("progress")]
    public class progress : BaseNotify
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

        [DbColumn("IdPermohonan")]
        public int IdPermohonan
        {
            get { return _idpermohonan; }
            set
            {
                SetProperty(ref _idpermohonan, value);
            }
        }

        [DbColumn("IdTahapan")]
        public int IdTahapan
        {
            get { return _idtahapan; }
            set
            {
                SetProperty(ref _idtahapan, value);
            }
        }

        private int _id;
        private int _idpermohonan;
        private int _idtahapan;
    }
}


