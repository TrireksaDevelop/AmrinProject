using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;




[assembly: Xamarin.Forms.Dependency(typeof(MobileApp.Services.InboxServices))]
namespace MobileApp.Services
{
    public class InboxServices : IDataStore<inbox>
    {
        private List<inbox> list;

        public async Task<bool> AddItemAsync(inbox item)
        {
            using (var res = new RestServices())
            {
                try
                {
                    item.Tanggal = DateTime.Now;
                    var result = await res.Post<inbox>("api/inbox",item);
                    if (res != null) {
                        list.Add(result);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<inbox> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public  Task<IEnumerable<inbox>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<inbox>> GetItemsAsync(int id)
        {
            using (var res = new RestServices())
            {
                list = await res.Get<List<inbox>>("api/inbox?id="+id);
                return list.OrderByDescending(O=>O.Tanggal);
            }
        }

        public Task<bool> UpdateItemAsync(inbox item)
        {
            throw new NotImplementedException();
        }
    }
}
