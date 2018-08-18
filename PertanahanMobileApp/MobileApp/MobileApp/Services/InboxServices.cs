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
        private bool isInstance = false;
        private List<InboxItem> list;

        public Task<bool> AddItemAsync(InboxItem item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<InboxItem> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InboxItem>> GetItemsAsync(bool forceRefresh = false)
        {
            if (isInstance)
                return Task.FromResult(list.AsEnumerable());
            else
            {
                list = new List<InboxItem>() {
                    new InboxItem{ Message="Apa Khabar ", RecieveDate=DateTime.Now, Sender="Admin", SenderDate=DateTime.Now, Readed=false, SenderInfo="Admin" },
                      new InboxItem{ Message="Apa Khabar 2", RecieveDate=DateTime.Now, Sender="Admin", SenderDate=DateTime.Now, Readed=false, SenderInfo="Admin" },
                        new InboxItem{ Message="Apa Khabar 3", RecieveDate=DateTime.Now, Sender="Admin", SenderDate=DateTime.Now, Readed=true, SenderInfo="Admin" },
                          new InboxItem{ Message="Apa Khabar 4", RecieveDate=DateTime.Now, Sender="Admin", SenderDate=DateTime.Now, Readed=true, SenderInfo="Admin" }
                };


                return Task.FromResult(list.AsEnumerable<InboxItem>());

            }
        }

        public Task<bool> UpdateItemAsync(InboxItem item)
        {
            throw new NotImplementedException();
        }
    }
}
