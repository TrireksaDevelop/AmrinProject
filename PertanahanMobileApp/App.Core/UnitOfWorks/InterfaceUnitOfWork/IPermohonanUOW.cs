using System.Collections.Generic;
using AppCore.ModelDTO;

namespace AppCore.UnitOfWorks.InterfaceUnitOfWork
{
    public interface IPermohonanUOW
    {
        permohonan InsertNewPermohonan(permohonan item,layanan lay);
        permohonan GetPermohonan(pemohon pemohon, StatusPermohonan status);
        List<progress> GetItemsTahapan(permohonan permohonan);
        List<permohonan> GetDaftarPermohonan(pemohon t);
        List<kelengkapan> GetKelengkapan(permohonan item);
        pemohon GetPemohon(int id);
    }
}