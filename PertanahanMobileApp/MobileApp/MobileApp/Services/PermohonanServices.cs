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
        private permohonan lastPermohonan { get; set; }

        public async Task<bool> CreateNewPermohonan(permohonan item)
        {
            try
            {
                using(var rest = new RestServices())
                {
                    var result = await rest.Post<permohonan>("api/permohonan", Helper.Content(item));
                    if(result!=null)
                    {
                        lastPermohonan = result;
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

        public Task<permohonan> GetLastPermohonan()
        {
            return Task.FromResult(lastPermohonan);
        }

        public Task<IEnumerable<permohonan>> GetPermohonans()
        {
            throw new NotImplementedException();
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
