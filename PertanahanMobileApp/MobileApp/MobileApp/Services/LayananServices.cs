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

        public async Task<IEnumerable<layanan>> GetItemsAsync(bool forceRefresh = false)
        {
            if (isInstance)
                return list;
            else
            {
                isInstance = true;
                using (var res = new RestServices())
                {
                    list = await res.Get<List<layanan>>("api/layanan");
                    return list;
                }
            }


        }

        public Task<bool> UpdateItemAsync(layanan item)
        {
            throw new NotImplementedException();
        }
    }
}
