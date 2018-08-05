using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using AppCore.Extentions;

namespace AppCore.Services
{
   public class TahapanService:ITahapanService
    {
        private layanan _layanan;
        public layanan Layanan => _layanan;
        public TahapanService(layanan item)
        {
            _layanan = item;
        }




       
    }
}
