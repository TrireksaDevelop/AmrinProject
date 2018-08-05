using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace AppCore.ModelDTO 
{ 
     [TableName("layanan")] 
     public class layanan:BaseNotify
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
SetProperty(ref _id, value);
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
SetProperty(ref _nama, value);
                     }
          } 

          [DbColumn("IdKategoriLayanan")] 
          public int IdKategoriLayanan
          { 
               get{return _kategorilayanan_id;} 
               set{ 
SetProperty(ref _kategorilayanan_id, value);
                     }
          }

        public kategorilayanan Kategori { get; set; }
        public List<tahapan> Tahapans { get; set; }
        public List<persyaratan> Persyaratans { get; set; }

        private int  _id;
           private string  _nama;
           private int  _kategorilayanan_id;
      }
}


