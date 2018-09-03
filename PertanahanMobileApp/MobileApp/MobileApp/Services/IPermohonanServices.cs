using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Services
{
    public interface IPermohonanServices
    {
       Task<permohonan> GetLastPermohonan();
      Task<ProgressBar> GetProgress();
        Task<tahapan> NextTahapan();
        Task<IEnumerable<permohonan>> GetPermohonans();
        Task<MessageModel> GetLastMessage();
        Task<bool> CreateNewPermohonan(permohonan item);
    }
}
