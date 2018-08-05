using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.UnitOfWorks.InterfaceUnitOfWork
{
    public interface IPersyaratanUOW
    {
        //Persyaratan

        List<persyaratan> GetPersyaratans(layanan t);
        persyaratan GetPersyartan(int Id);
        persyaratan InsertPersyaratan(persyaratan t);
        persyaratan UpdatePersyaratan(persyaratan t);
        bool DeletePersyaratan(int Id);
    }
}
