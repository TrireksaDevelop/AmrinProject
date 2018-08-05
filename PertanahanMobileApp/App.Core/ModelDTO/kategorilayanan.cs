using System.Collections.Generic;
using Ocph.DAL;
namespace AppCore.ModelDTO
{
    [TableName("kategorilayanan")]
    public class kategorilayanan : BaseNotify
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

        [DbColumn("Nama")]
        public string Nama
        {
            get { return _nama; }
            set
            {
                SetProperty(ref _nama, value);
            }
        }

        public List<layanan> Layanans { get; set; }

        private int _id;
        private string _nama;
    }
}


