using System;
using System.Collections.Generic;
using System.Text;
using AppCore.ModelDTO;

namespace AppCore.UnitOfWorks.InterfaceUnitOfWork
{
    public interface IBidangUOW
    {
        bidang GetBidangTugas(petugas petugas);
        List<tahapan> GetTahapanTugasBidang(bidang bidangTugas);
        List<permohonan> GetAllPermohonan(bidang bidangTugas);
        bool ChangeWork(permohonan permohonan, progress tahapan);
    }
}
