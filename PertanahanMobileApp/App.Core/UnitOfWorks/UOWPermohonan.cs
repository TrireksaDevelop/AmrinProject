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
        public List<permohonan> GetDaftarPermohonan(pemohon pemohon)
        {

            using (var db = new OcphDbContext())
            {
                return db.Permohonans.Where(O => O.IdPemohon == pemohon.Id).ToList();
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
                catch (Exception)
                {
                    trans.Rollback();
                    return null;
                }
            }
        }
    }
}
