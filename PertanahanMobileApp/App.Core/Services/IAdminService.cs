using System.Collections.Generic;
using AppCore.ModelDTO;
namespace AppCore.Services
{
    public interface IAdminService
    {
        List<permohonan> GetPermohonans();
        List<tahapan> GetTahapans();
        List<kelengkapan> GetKelengkapans(permohonan item);
        bool ChangeWork(permohonan permohonan,progress tahapan);
        void SendMessageToPemohon(string message);
        IPermohonanService PermohonanService { get; set; }
        bidang BidangTugas { get; set; }


    }
}