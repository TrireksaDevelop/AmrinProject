using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AppCore.ModelDTO;
using AppCore.Services;

namespace AppCore.Extentions
{
   public static class LayananExtention
    {

        public static ILayananService service = new LayananService();

        public static List<tahapan> GetTahapans(this layanan item)
        {
            return service.GetTahapans(item);
        }
    }
}
