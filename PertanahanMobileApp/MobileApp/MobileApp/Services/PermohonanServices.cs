using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;

[assembly: Xamarin.Forms.Dependency(typeof(MobileApp.Services.PermohonanServices))]

namespace MobileApp.Services
{
    public class PermohonanServices : IPermohonanServices
    {
        private bool isInstance;
        private permohonan lastPermohonan { get; set; }
        private List<permohonan> list { get; set; }

        public Task Clean()
        {
            isInstance = false;
            lastPermohonan = null;
            list = null;
            return Task.FromResult(0);
        }

        public async Task<bool> CreateNewPermohonan(permohonan item)
        {
            try
            {
                using(var rest = new RestServices())
                {
                    var result = await rest.Post<permohonan>("api/ClientPermohonan", item);
                    if(result!=null)
                    {
                        lastPermohonan = result;
                        if (list == null)
                            list = new List<permohonan>();

                        list.Add(lastPermohonan);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public Task<MessageModel> GetLastMessage()
        {
            throw new NotImplementedException();
        }

        public async Task<permohonan> GetLastPermohonan()
        {
            try
            {
                if(lastPermohonan==null)
                {
                    using (var rest = new RestServices())
                    {
                        var result = await rest.Get<permohonan>("api/ClientPermohonan/last");
                        if (result != null)
                        {
                            lastPermohonan = result;
                        }
                    }
                }
                return lastPermohonan;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
          
        }

        public async Task<permohonan> GetPermohonanById(int id)
        {
            try
            {
                using (var rest = new RestServices())
                {
                    var result = await rest.Get<permohonan>("api/ClientPermohonan/"+id);
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public async Task<IEnumerable<permohonan>> GetPermohonans()
        {
            try
            {
              if(!isInstance)
                {
                    isInstance = true;
                    list = new List<permohonan>();
                    using (var rest = new RestServices())
                    {
                        var result = await rest.Get<List<permohonan>>("api/ClientPermohonan");
                        if (result != null)
                        {
                           foreach(var item in result)
                            {
                                list.Add(item);
                            }
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public Task<ProgressBar> GetProgress()
        {
            throw new NotImplementedException();
        }

        public Task<tahapan> NextTahapan()
        {
            throw new NotImplementedException();
        }


    }
}
