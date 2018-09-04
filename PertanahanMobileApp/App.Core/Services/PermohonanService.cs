using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.ModelDTO;
using AppCore.UnitOfWorks;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;

namespace AppCore.Services
{
    public class PermohonanService : IPermohonanService
    {
        private List<progress> _tahapans;

        private tahapan _currentTahapan;

        public IPermohonanUOW UnitWorkPermohonan { get; set; }

        public pemohon Pemohon { get; private set; }

        public permohonan Permohonan { get; private  set; }

        public PermohonanService(pemohon t,IPermohonanUOW uow)
        {
            Pemohon = t;
            UnitWorkPermohonan = uow;
            var result= UnitWorkPermohonan.GetDaftarPermohonan(t);
            if(result.Count>0)
            {
                Permohonan = result.Last();
                _tahapans = UnitWorkPermohonan.GetItemsTahapan(Permohonan);
            }
        
        }

        public PermohonanService(IPermohonanUOW uOWPermohonan)
        {
            this.UnitWorkPermohonan= uOWPermohonan;
        }

        public List<progress> ItemsTahapan()
        {
            if (Permohonan != null)
            {
                _tahapans= UnitWorkPermohonan.GetItemsTahapan(Permohonan);
                return _tahapans;
            }
            else
                throw new SystemException("Tentukan permohonan");
        }

        

        public tahapan GetCurrentTahapan()
        {
           return UnitWorkPermohonan.GetCurrentTahapan(Permohonan);
        }

       
        public tahapan GetNextTahapan()
        {
            return UnitWorkPermohonan.GetNextTahapan(Permohonan);
        }

        
        public permohonan CreatePermohonan(layanan t)
        {
            if (t == null || t.Id <= 0)
                throw new ArgumentNullException("layanan", "layanan IsNull atau Id Layanan 0");
            else if (Pemohon == null)
            {
                throw new ArgumentNullException("Pemohon", "Pemohon Tidak Ada");
            }
            else
            {
                var itemPermohonan = new permohonan { IdLayanan = t.Id, IdPemohon = Pemohon.Id, Status =  StatusPermohonan.Baru };
                var result = UnitWorkPermohonan.InsertNewPermohonan(itemPermohonan, t);
                if(result!=null)
                {
                    this.Permohonan = itemPermohonan;
                    return itemPermohonan;
                }else
                {
                    throw new ArgumentNullException("permohonan", "Permohonan Tidak Berhasil Dibuat");
                }
            }
        }

        public List<permohonan> GetPermohonans()
        {
           if(Pemohon==null)
                throw new SystemException("Pemohon Tidak Ada");
           else
            {
                return UnitWorkPermohonan.GetDaftarPermohonan(Pemohon);
            }
        }

        public void SetCurrentPermohonan(permohonan item)
        {
            Permohonan = item;
            if (item==null)
            {
                _tahapans = null;
                _currentTahapan = null;
            }
                
        }

        public List<kelengkapan> GetKelengkapan(permohonan item)
        {
            if (item == null)
                throw new SystemException("Permohonan Tidak Ada");
            else
                return UnitWorkPermohonan.GetKelengkapan(item);

        }

        public pemohon GetPemohon(int Id)
        {
            if (Id <= 0)
                throw new SystemException("Id Tidak Boleh 0");
            else
            {
                pemohon result = UnitWorkPermohonan.GetPemohon(Id);
                if (result != null)
                {
                    Pemohon = result;
                    return result;
                }
                else
                    throw new SystemException("Pemohon Tidak Ditemukan");

            }
        }

        public permohonan GetPermohonan(int Id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Permohonans.Where(O => O.Id == Id).FirstOrDefault();
                    if (result != null)
                    {



                        return result;
                    }
                    else
                        throw new SystemException("Data Tidak Ditemukan");
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public bool SetNextStep()
        {
          return  UnitWorkPermohonan.SetNextStep(Permohonan, Permohonan.NextTahapan);
        }

        public Task<bool> UpdatePermohonan(permohonan item)
        {

            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {

                    var dbKelengkapan = db.Kelengkapans.Where(O => O.IdPermohonan == item.Id).ToList();

                    if (item.Kelengkapans!=null)
                    {
                        foreach (var data in item.Kelengkapans)
                        {
                            var result = dbKelengkapan.Where(O => O.IdPersyaratan == data.IdPersyaratan && O.IdPermohonan == item.Id).FirstOrDefault();
                            if (result == null)
                            {
                                if (!db.Kelengkapans.Insert(data))
                                    throw new SystemException("Data Tidak Tersimpan");
                            }else
                            {
                                if(!db.Kelengkapans.Update(O=> new { O.Status },data, O=>O.IdPermohonan==data.IdPermohonan && O.IdPersyaratan==data.IdPersyaratan))
                                {
                                    throw new SystemException("Data Tidak Tersimpan");
                                }
                            }
                        }
                    }

                    var dbTahapans = db.Kelengkapans.Where(O => O.IdPermohonan == item.Id).ToList();


                    if (item.Tahapans!=null )
                    {
                        if (item.Tahapans != null)
                        {
                            foreach (var data in item.Tahapans)
                            {
                                var result = dbTahapans.Where(O => O.Id == data.IdTahapan && O.IdPermohonan == item.Id).FirstOrDefault();
                                if (result == null)
                                {
                                    if (!db.Progress.Insert(data))
                                        throw new SystemException("Data Tidak Tersimpan");
                                }
                            }
                        }
                    }
                    trans.Commit();
                    return Task.FromResult(true);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }
            }

           
        }
    }
}
