using AppCore.ModelDTO;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore.UnitOfWorks
{
    public class PersyaratanUOW : IPersyaratanUOW
    {

        public List<persyaratan> GetPersyaratans(layanan t)
        {
            if (t == null)
                throw new ArgumentNullException("layanan", "Layanan IsNUll");
            else
            {

                using (var db = new OcphDbContext())
                {
                    return db.Persyaratans.Where(O => O.IdLayanan == t.Id).ToList();
                }
            }
        }

        public persyaratan GetPersyartan(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("Id", "Set Persyaratan Id");
            else
            {
                using (var db = new OcphDbContext())
                {
                    return db.Persyaratans.Where(O => O.Id == Id).FirstOrDefault();
                }
            }
        }

        public persyaratan InsertPersyaratan(persyaratan t)
        {
            if (t == null)
                throw new ArgumentNullException("persyaratan", "Persyaratan IsNUll");
            else
            {
                using (var db = new OcphDbContext())
                {
                    t.Id = db.Persyaratans.InsertAndGetLastID(t);
                    if (t.Id > 0)
                        return t;
                    else
                        return null;
                }
            }
        }

        public persyaratan UpdatePersyaratan(persyaratan t)
        {
            if (t == null)
                throw new ArgumentNullException("persyaratan", "persyaratan IsNull");
            else
            {
                using (var db = new OcphDbContext())
                {
                    var isUpdated = db.Persyaratans.Update(O => new { O.Nama, O.Keterangan }, t, O => O.Id == t.Id);
                    if (isUpdated)
                        return t;
                    else
                        return null;
                }
            }
        }

        public bool DeletePersyaratan(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("Pilih Kategori", "Id");
            else
            {
                using (var db = new OcphDbContext())
                {
                    return db.Persyaratans.Delete(O => O.Id == Id);
                }
            }
        }

    }
}
