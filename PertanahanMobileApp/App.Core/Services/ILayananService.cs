using AppCore.ModelDTO;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public interface ILayananService
    {
        List<layanan> GetLayanan();
        layanan GetLayananById(int Id);
        layanan InsertLayanan(layanan t);
        layanan UpdateLayanan(layanan t);
        bool DeleteLayanan(int Id);
        List<tahapan> GetTahapans(layanan t);

        //Kategories
        List<kategorilayanan> GetKategories();
        kategorilayanan GetKategory(int Id);
        kategorilayanan InsertKategory(kategorilayanan t);
        kategorilayanan UpdateKategory(kategorilayanan t);
        bool DeleteKategory(int Id);


        //Persyaratan

        List<persyaratan> GetPersyaratans(layanan t);
        persyaratan GetPersyartan(int Id);
        persyaratan InsertPersyaratan(persyaratan t);
        persyaratan UpdatePersyaratan(persyaratan t);
        bool DeletePersyaratan(int Id);

        IPersyaratanUOW UnitOfWorkPersyaratan { get; set; }

        List<tahapan> UpdateTahapans(int id, List<tahapan> value);
    }
}
