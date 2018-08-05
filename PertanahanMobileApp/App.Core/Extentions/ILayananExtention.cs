using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Extentions
{
    public interface ILayananExtention
    {
        List<tahapan> GetTahapans<T>(T t);

    }
}
