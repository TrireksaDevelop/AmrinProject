using MobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
    public class RestServices : HttpClient, IDisposable
    {
        public RestServices()
        {
            this.MaxResponseContentBufferSize = 25600000;
            this.BaseAddress = new Uri(Helper.Server);
         
            SetHeader();
        }

        private async void SetHeader()
        {
            var token = await Helper.GetToken();
            if(token!=null)
            {
              //  this.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token.token);
            }
        }

        public async Task<T> Post<T>(string uri, object t) where T : class
        {
            try
            {
                await Task.Delay(200);
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

        public async Task<T> Get<T>(string uri) 
        {
            try
            {
                await Task.Delay(200);
                var result = await GetAsync(uri);
                var responseText = await result.Content.ReadAsStringAsync();
                var obj = Activator.CreateInstance<T>();
                if (result.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(responseText);
                }
                else
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

        public async Task<T> Put<T>(string uri, T t)
        {
            try
            {
                await Task.Delay(200);
                var result = await PutAsync(uri, Helper.Content(t));
                var responseText = await result.Content.ReadAsStringAsync();
                var obj = Activator.CreateInstance<T>();
                if (result.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<T>(responseText);
                }
                else
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
