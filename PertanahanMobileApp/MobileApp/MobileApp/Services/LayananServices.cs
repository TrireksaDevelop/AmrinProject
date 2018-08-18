using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(MobileApp.Services.LayananServices))]

namespace MobileApp.Services
{
    public class LayananServices : IDataStore<layanan>
    {
        private bool isInstance;
        private List<layanan> list;

        public Task<bool> AddItemAsync(layanan item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<layanan> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<layanan>> GetItemsAsync(bool forceRefresh = false)
        {
            if (isInstance)
                return Task.FromResult(list.AsEnumerable());
            else
            {
                list = new List<layanan>() {
                    new layanan{Nama="Waris", Kategori=new kategorilayanan{ Nama="Peralihan" }, Keterangan="Layanan Memberikan Warisan " },
                     new layanan{Nama="Hiba", Kategori=new kategorilayanan{ Nama="Peralihan" }, Keterangan="Layanan Hiba " },
                      new layanan{Nama="Pemecahan", Kategori=new kategorilayanan{ Nama="Peralihan" }, Keterangan="Layanan Pemecahan " , Tahapans=new List<tahapan>{
                           new tahapan{ Nama="Pendaftaran", Keterangan="apakek" , Urutan=1},
                           new tahapan{ Nama="Validasi", Keterangan="apakek", Urutan=2 },
                           new tahapan{ Nama="Pengukuran", Keterangan="apakek", Urutan=3 },
                           new tahapan{ Nama="Pencetakan", Keterangan="apakek",Urutan=4 }

                      } }
                };


                return Task.FromResult(list.AsEnumerable<layanan>());

            }
        }

        public Task<bool> UpdateItemAsync(layanan item)
        {
            throw new NotImplementedException();
        }
    }
}
