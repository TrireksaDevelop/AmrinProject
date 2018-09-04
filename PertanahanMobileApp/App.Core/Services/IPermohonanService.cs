using AppCore.ModelDTO;
using AppCore.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public interface IPermohonanService
    {
        List<progress> ItemsTahapan();
        tahapan GetCurrentTahapan();
        tahapan GetNextTahapan();
        permohonan CreatePermohonan(layanan t);
        List<permohonan> GetPermohonans();
        void SetCurrentPermohonan(permohonan item);
        List<kelengkapan> GetKelengkapan(permohonan item);
        pemohon GetPemohon(int Id);
        permohonan GetPermohonan(int Id);
        bool SetNextStep();
        Task<bool> UpdatePermohonan(permohonan item);
    }
}
