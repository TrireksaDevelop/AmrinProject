using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public interface IMasterService
    {
        petugas SaveChange(petugas pet);
        bool DeletePetugas(int pet);
        List<petugas> GetPetugas();
        petugas GetPetugasById(int id);


        //bidang

        bidang SaveChange(bidang item);
        bool DeleteBidang(int item);
        List<bidang> GetBidang();
        bidang GetBidangById(int id);


        /// Tahapan
        /// 
        tahapan SaveChange(tahapan item);
        bool DeleteTahapan(int item);
        List<tahapan> GetTahapan();
        tahapan GetTahapanById(int id);

    }
}
