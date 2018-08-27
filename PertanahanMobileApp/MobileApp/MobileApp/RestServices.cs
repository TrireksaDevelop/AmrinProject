using MobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
    public class RestServices : HttpClient, IDisposable
    {
      

        public RestServices()
        {
            this.BaseAddress = new Uri(Helper.Server);
        }

        public void Dispose()
        {
            
        }

        public async Task<T> Post<T>(string uri, object t) where T : class
        {
            try
            {
                var result = await PostAsync(uri, Helper.Content(t));
                var responseText = await result.Content.ReadAsStringAsync();
                var obj = Activator.CreateInstance<T>();
                if (result.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(responseText);
                }else
                {
                    throw new SystemException(responseText);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }
    }
    
}
