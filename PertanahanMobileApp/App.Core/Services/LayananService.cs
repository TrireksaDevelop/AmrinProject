using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;

namespace AppCore.Services
{
    public class LayananService : ILayananService
    {
        public IPersyaratanUOW UnitOfWorkPersyaratan { get; set; }
        public LayananService() {
            UnitOfWorkPersyaratan = new AppCore.UnitOfWorks.PersyaratanUOW();

        }
        public LayananService(IPersyaratanUOW unitOfWork)
        {
            UnitOfWorkPersyaratan = unitOfWork;
        }

        #region Layanan
        public bool DeleteLayanan(int Id)
        {
            using (var db = new OcphDbContext())
            {
                if (Id <= 0)
                    throw new ArgumentException("Pilih Layanan", "Id");
                else
                {
                    return db.Layanans.Delete(O => O.Id == Id);
                }
            }
        }

        public List<layanan> GetLayanan()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var layanans = (from a in db.Layanans.Select()
                                   join b in db.Kategories.Select() on a.IdKategoriLayanan equals b.Id
                                   join d in db.Persyaratans.Select() on a.Id equals d.IdLayanan into dGroup
                                   from d in dGroup.DefaultIfEmpty()
                                   select new layanan
                                   {
                                       Id = a.Id,
                                       IdKategoriLayanan = a.Id,
                                       Nama = a.Nama,
                                       Kategori = b,
                                       Persyaratans = dGroup.ToList()
                                   }).ToList();

                    foreach(var item in layanans)
                    {
                        item.Tahapans = (from z in db.TahapanLayanan.Where(O => O.Id == item.Id)
                                        join y in db.Tahapans.Select() on z.TahapanId equals y.Id
                                        join x in db.Bidangs.Select() on y.BidangId equals x.Id
                                        select new tahapan
                                        {
                                            Bidang = x, 
                                            BidangId = y.BidangId, LayananId=z.Id,
                                            Id = y.Id,
                                            Keterangan = y.Keterangan,
                                            Nama = y.Nama,Urutan = z.Urutan
                                        }).ToList();
                                       
                    }

                  
                    return layanans;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public layanan GetLayananById(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("Id Tidak Boleh 0", "Id");
            }
            else
            {
                using (var db = new OcphDbContext())
                {
                    try
                    {
                        var layanans = (from a in db.Layanans.Where(O=>O.Id==Id)
                                        join b in db.Kategories.Select() on a.IdKategoriLayanan equals b.Id
                                        join d in db.Persyaratans.Select() on a.Id equals d.IdLayanan into dGroup
                                        from d in dGroup.DefaultIfEmpty()
                                        select new layanan
                                        {
                                            Id = a.Id,
                                            IdKategoriLayanan = a.Id,
                                            Nama = a.Nama,
                                            Kategori = b,
                                            Persyaratans = dGroup.ToList()
                                        }).ToList();

                        foreach (var item in layanans)
                        {
                            item.Tahapans = (from z in db.TahapanLayanan.Where(O => O.Id == item.Id)
                                             join y in db.Tahapans.Select() on z.TahapanId equals y.Id
                                             join x in db.Bidangs.Select() on y.BidangId equals x.Id
                                             select new tahapan
                                             {
                                                 Bidang = x,
                                                 BidangId = y.BidangId,
                                                 LayananId = z.Id,
                                                 Id = y.Id,
                                                 Keterangan = y.Keterangan,
                                                 Nama = y.Nama,
                                                 Urutan = z.Urutan
                                             }).ToList();

                        }


                        return layanans.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        throw new SystemException(ex.Message);
                    }
                   
                }
            }
        }

        public List<tahapan> GetTahapans(layanan t)
        {
            using (var db = new OcphDbContext())
            {
                if (t == null)
                    throw new ArgumentNullException(t.GetType().Name, " IsNull");
                else
                    return db.Tahapans.Where(O => O.BidangId == t.Id).ToList();
            }
        }

        public layanan InsertLayanan(layanan t)
        {
            using (var db = new OcphDbContext())
            {
                if (t == null)
                    throw new ArgumentNullException(t.GetType().Name, " IsNull");
                else
                {
                    t.Id = db.Layanans.InsertAndGetLastID(t);
                    if (t.Id > 0)
                        return t;
                    else
                        return null;
                }
            }
        }

        public layanan UpdateLayanan(layanan t)
        {
            using (var db = new OcphDbContext())
            {
                if (t == null)
                {
                    throw new ArgumentNullException("layanan", "IsNull");
                }
                else
                {
                    var isUpdate = db.Layanans.Update(O => new { O.IdKategoriLayanan, O.Nama }, t, O => O.Id == t.Id);
                    if (isUpdate)
                        return t;
                    else
                        return null;
                }
            }
        }
        #endregion

        #region Kategories

        public List<kategorilayanan> GetKategories()
        {
            using (var db = new OcphDbContext())
            {
                return db.Kategories.Select().ToList();
            }
        }

        public kategorilayanan GetKategory(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("Pilih Kategori", "Id");
            else
            {
                using (var db = new OcphDbContext())
                {
                    var result = from a in db.Kategories.Where(O => O.Id == Id)
                                 join b in db.Layanans.Select() on a.Id equals b.IdKategoriLayanan into bGroup
                                 from b in bGroup.DefaultIfEmpty()
                                 select new kategorilayanan { Id = a.Id, Nama = a.Nama, Layanans = bGroup.ToList() };
                    return result.FirstOrDefault();
                }
            }
        }

        public bool DeleteKategory(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("Pilih Kategori", "Id");
            else
            {
                using (var db = new OcphDbContext())
                {
                    return db.Kategories.Delete(O => O.Id == Id);
                }
            }
        }
        public kategorilayanan UpdateKategory(kategorilayanan t)
        {
            if (t == null)
                throw new ArgumentNullException("Id", "Pilih Kategori");
            else
            {
                using (var db = new OcphDbContext())
                {
                    var isUpdated = db.Kategories.Update(O => new { O.Nama }, t, O => O.Id == t.Id);
                    if (isUpdated)
                        return t;
                    else
                        return null;
                }
            }
        }

        public kategorilayanan InsertKategory(kategorilayanan t)
        {
            if (t == null)
                throw new ArgumentNullException("Id", "Pilih Kategori");
            else
            {
                using (var db = new OcphDbContext())
                {
                    t.Id = db.Kategories.InsertAndGetLastID(t);
                    if (t.Id > 0)
                        return t;
                    else
                        return null;
                }
            }
        }
        #endregion

        #region Persyaratan
        public List<persyaratan> GetPersyaratans(layanan t)
        {
            if (t == null)
                throw new SystemException("Layanan Tidak Boleh Null");
            return UnitOfWorkPersyaratan.GetPersyaratans(t);
        }

        public persyaratan GetPersyartan(int Id)
        {
            if (Id <= 0)
                throw new SystemException("Id Tidak Boleh 0");
            else
                return UnitOfWorkPersyaratan.GetPersyartan(Id);
        }

        public persyaratan InsertPersyaratan(persyaratan t)
        {
            if (t == null)
                throw new SystemException("Persyaratan Tidak Boleh Null");
            else
            {
                return UnitOfWorkPersyaratan.InsertPersyaratan(t);
            }
        }

        public persyaratan UpdatePersyaratan(persyaratan t)
        {
            if (t == null)
                throw new SystemException("Persyaratan Tidak Boleh Null");
            else
                return UnitOfWorkPersyaratan.UpdatePersyaratan(t);
        }

        public bool DeletePersyaratan(int Id)
        {
            if (Id <= 0)
                throw new SystemException("Id Tidak Boleh 0");
            else
                return UnitOfWorkPersyaratan.DeletePersyaratan(Id);
        }

        public List<tahapan> UpdateTahapans(int Id, List<tahapan> values)
        {
            if (Id <= 0)
                throw new SystemException("Id Tidak Boleh 0");
            else
            {
                using (var db = new OcphDbContext())
                {
                    var trans = db.BeginTransaction();
                    try
                    {
                        var lay = this.GetLayananById(Id);
                        if (lay == null)
                            throw new SystemException("Data Layanan Tidak Ditemukan");
                        else
                        {
                            foreach (var item in values)
                            {
                                var source = lay.Tahapans.Where(O => O.Id == item.Id).FirstOrDefault();
                                if (source != null)
                                {
                                    if (!db.TahapanLayanan.Update(O => new { O.Urutan }, new tahapanlayanan { Id = source.LayananId, TahapanId = source.Id, Urutan = source.Urutan },
                                        O => O.TahapanId == source.Id && O.Id == source.LayananId))
                                        throw new SystemException("Data Tidak Tersimpan");
                                }
                                else
                                {
                                    if (!db.TahapanLayanan.Insert(new tahapanlayanan { Id = item.LayananId, TahapanId = item.Id, Urutan = item.Urutan }))
                                        throw new SystemException("Data Tidak Tersimpan");
                                }
                            }

                            foreach (var item in lay.Tahapans)
                            {
                                var source = values.Where(O => O.Id == item.Id).FirstOrDefault();
                                if (source == null)
                                {
                                    if (!db.TahapanLayanan.Delete(O => O.TahapanId == item.Id && O.Id == item.LayananId))
                                        throw new SystemException("Data Tidak Tersimpan");
                                }
                            }
                            trans.Commit();
                            return values;
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new SystemException(ex.Message);
                    }

                }
            }
        }
        #endregion
    }
}