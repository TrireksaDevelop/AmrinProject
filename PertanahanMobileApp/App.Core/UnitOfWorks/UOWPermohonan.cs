using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppCore.ModelDTO;
using AppCore.Services;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;

namespace AppCore.UnitOfWorks
{
    public class UOWPermohonan : IPermohonanUOW
    {
        public tahapan GetCurrentTahapan(permohonan permohonan)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var lastProggress = db.Progress.Where(O => O.IdPermohonan == permohonan.Id).OrderBy(O=>O.Id).Last();

                    if(lastProggress==null)
                    {
                        return null;
                    }else
                    {
                        var Curenttahapans = from z in db.TahapanLayanan.Where(O => O.Id == permohonan.IdLayanan && O.TahapanId == lastProggress.IdTahapan)
                                             join y in db.Tahapans.Select() on z.TahapanId equals y.Id
                                             select y;
                        return Curenttahapans.FirstOrDefault();
                    }
                 
                }
            }
            catch
            {
                return null;
                
            }
        }


        public tahapan GetNextTahapan(permohonan permohonan)
        {
            try
            {
                var current = GetCurrentTahapan(permohonan);
               
                using (var db = new OcphDbContext())
                {
                    int urutan = 1;
                    if (current != null)
                    {
                        var lastTahapan = db.TahapanLayanan.Where(O => O.Id == permohonan.IdLayanan && O.TahapanId == current.Id).FirstOrDefault();
                        urutan = lastTahapan.Urutan + 1;
                    }
                    var Curenttahapans = from z in db.TahapanLayanan.Where(O => O.Id == permohonan.IdLayanan && O.Urutan == urutan)
                                         join y in db.Tahapans.Select() on z.TahapanId equals y.Id
                                         select y;

                    return Curenttahapans.FirstOrDefault();

                }




            }
            catch
            {
                return null;

            }
        }

        public List<permohonan> GetDaftarPermohonan(pemohon pemohon)
        {

            using (var db = new OcphDbContext())
            {

                var results = from a in db.Permohonans.Where(O => O.IdPemohon == pemohon.Id)
                              join b in db.Layanans.Select() on a.IdLayanan equals b.Id
                              join c in db.Kategories.Select() on b.IdKategoriLayanan equals c.Id
                              select new permohonan {
                                   Id = a.Id, IdLayanan=a.IdLayanan, IdPemohon=a.IdPemohon, Status=a.Status,
                                    Layanan = new layanan { Id=b.Id, IdKategoriLayanan=b.IdKategoriLayanan, Kategori=c, Nama=b.Nama }
                              };

                return results.ToList();
            }
        }

        public List<progress> GetItemsTahapan(permohonan permohonan)
        {
            using (var db = new OcphDbContext())
            {
                return db.Progress.Where(O => O.IdPermohonan == permohonan.Id).ToList();
            }
        }

        public List<kelengkapan> GetKelengkapan(permohonan item)
        {

            using (var db = new OcphDbContext())
            {
                return db.Kelengkapans.Where(O => O.IdPermohonan == item.IdPemohon).ToList();
            }
        }

        public pemohon GetPemohon(int id)
        {

            using (var db = new OcphDbContext())
            {
                return db.Pemohons.Where(O => O.Id == id).FirstOrDefault();
            }
        }

        public permohonan GetPermohonan(pemohon pemohon, StatusPermohonan status)
        {
            using (var db = new OcphDbContext())
            {
                return db.Permohonans.Where(O => O.IdPemohon == pemohon.Id && O.Status == status).FirstOrDefault();
            }
        }

        public permohonan InsertNewPermohonan(permohonan itemPermohonan,layanan lay)
        {

            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    //Tahapan
                    itemPermohonan.Id = db.Permohonans.InsertAndGetLastID(itemPermohonan);
                  
                    if (itemPermohonan.Id > 0)
                    {
                        var layananService = new LayananService();
                        var tahapans = layananService.GetTahapans(lay);
                        List<progress> TahapanDetails = new List<progress>();
                        foreach (var item in tahapans)
                        {
                            var tahap = new progress { IdPermohonan = itemPermohonan.Id, IdTahapan = item.Id };
                            tahap.Id = db.Progress.InsertAndGetLastID(tahap);
                            if (tahap.Id <= 0)
                                throw new SystemException("Tahapan Tidak Tersimpan");
                            else
                                TahapanDetails.Add(tahap);
                        }
                        itemPermohonan.Tahapans = TahapanDetails;

                         trans.Commit();
                        return itemPermohonan;
                    }
                    else
                    {
                        throw new SystemException("Permohonan Baru Tidak Dapat Dibuat");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return null;
                }
            }
        }

        public bool SetNextStep(permohonan p, tahapan nextTahapan)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Progress.Insert(new progress { IdPermohonan = p.Id, IdTahapan = nextTahapan.Id });
                return result;
            }
        }
    }
}
