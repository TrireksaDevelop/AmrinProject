using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;




[assembly: Xamarin.Forms.Dependency(typeof(MobileApp.Services.InboxServices))]
namespace MobileApp.Services
{
    public class InboxServices : IDataStore<InboxItem>
    {
        private List<InboxItem> list;

        public async Task<bool> AddItemAsync(InboxItem item)
        {
            using (var res = new RestServices())
            {
                try
                {
                    var result = await res.Post<InboxItem>("api/inbox", Helper.Content(item));
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

        public Task<InboxItem> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public  Task<IEnumerable<InboxItem>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InboxItem>> GetItemsAsync(int id)
        {
            using (var res = new RestServices())
            {
                list = await res.Get<List<InboxItem>>("api/inbox?id="+id);
                return list;
            }
        }

        public Task<bool> UpdateItemAsync(InboxItem item)
        {
            throw new NotImplementedException();
        }
    }
}
