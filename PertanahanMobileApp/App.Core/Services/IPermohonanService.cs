using AppCore.ModelDTO;
using AppCore.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public interface IPermohonanService
    {
        List<progress> ItemsTahapan();
        void SetCurrentTahapan(progress item);
        progress GetCurrentTahapan();
        progress GetLastTahapan();
        progress GetNextTahapan();
        bool CreatePermohonan(layanan t);
        List<permohonan> GetPermohonans();
        void SetCurrentPermohonan(permohonan item);
        List<kelengkapan> GetKelengkapan(permohonan item);
        pemohon GetPemohon(int Id);
    }
}
