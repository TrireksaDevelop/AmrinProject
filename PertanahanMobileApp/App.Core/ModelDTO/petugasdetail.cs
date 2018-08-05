using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;

namespace AppCore.ModelDTO
{
    [TableName("petugasdetail")]
    public class petugasdetail : BaseNotify
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

        [DbColumn("PetugasId")]
        public int PetugasId
        {
            get { return _petugasid; }
            set
            {
                SetProperty(ref _petugasid, value);
            }
        }

        [DbColumn("Bidang_Id")]
        public int Bidang_Id
        {
            get { return _bidang_id; }
            set
            {
                SetProperty(ref _bidang_id, value);
            }
        }

        private int _id;
        private int _petugasid;
        private int _bidang_id;
    }
}


