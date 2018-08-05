using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore.Contexts
{
    public class PermohonanContext : IPermohonanContext
    {
        private permohonan _permohonan;
        private IQueryable<permohonan> _permohonans;

        public pemohon Pemohon { get; }

        public PermohonanContext(pemohon pemohon)
        {
            this.Pemohon = pemohon;
        }

        public permohonan GetPermohonan(StatusPermohonan status)
        {
            RequestIsValid();
            if (_permohonan == null)
            {
                using (var db = new OcphDbContext())
                {
                    _permohonan = db.Permohonans.Where(O => O.Status == status).FirstOrDefault();
                }
            }
            return _permohonan;
        }

        private void RequestIsValid()
        {
            if (Pemohon == null || Pemohon.Id == 0)
                throw new SystemException("Pemohon Belum Ditentukan");
        }

        public permohonan GetLastPermohonan()
        {
            RequestIsValid();
            using (var db = new OcphDbContext())
            {
                _permohonan = db.Permohonans.Where(O => O.IdPemohon == Pemohon.Id).LastOrDefault();
            }

            return _permohonan;
        }

        public IEnumerable<permohonan> GetPermohonans()
        {
            RequestIsValid();
            using (var db = new OcphDbContext())
            {
                if (_permohonans == null)
                {
                    var result = from a in db.Permohonans.Where(O => O.IdPemohon == Pemohon.Id)
                                 join b in db.Progress.Select() on a.Id equals b.IdPermohonan into bGrup
                                 from b in bGrup.DefaultIfEmpty()
                                 join c in db.Kelengkapans.Select() on a.IdPemohon equals c.IdPermohonan into cGroup
                                 from c in cGroup.DefaultIfEmpty()
                                 select new permohonan { Kelengkapans =cGroup.ToList(), Tahapans=bGrup.ToList(), Id=a.Id, };

                    _permohonans = db.Permohonans.Where(O => O.IdPemohon == Pemohon.Id);
                }
                return _permohonans;
            }
        }

        public bool CreatePermohonan(permohonan t)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    if (CreatedIsValid(t))
                    {
                        t.IdPemohon = Pemohon.Id;
                        t.Id = db.Permohonans.InsertAndGetLastID(t);
                        if(t.Id>0)
                        {
                            t.Kelengkapans = new List<kelengkapan>();
                            var layanans = db.Persyaratans.Where(O => O.IdLayanan == t.IdLayanan);
                            foreach (var item in layanans)
                            {
                                var kelengkapan = new kelengkapan { IdPermohonan = t.Id, IdPersyaratan = item.Id, Status = StatusKelengkapan.Tidak };
                                kelengkapan.Id = db.Kelengkapans.InsertAndGetLastID(kelengkapan);
                                if (kelengkapan.Id <= 0)
                                    throw new SystemException("Gagal Tambah Kelengkapan ");
                                else
                                {
                                    t.Kelengkapans.Add(kelengkapan);
                                }
                            }


                            t.Tahapans = new List<progress>();
                            var tahapans = db.Tahapans.Where(O => O.BidangId == t.IdLayanan);
                            foreach(var item in tahapans)
                            {
                                var tahap = new progress { IdPermohonan = t.Id, IdTahapan = item.Id };
                                tahap.Id = db.Progress.InsertAndGetLastID(tahap);
                                if (tahap.Id > 0)
                                    t.Tahapans.Add(tahap);
                                else
                                {
                                    throw new SystemException("Gagal Tambah Tahapan");
                                }
                            }
                            _permohonan = t;

                            trans.Commit();
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        throw new SystemException("Data Tidak Valid");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);
                }

            }
        }

        private bool CreatedIsValid(permohonan t)
        {
            if (Pemohon == null)
                throw new SystemException("Pemohon Belum Ditentukan");
            if (t.IdLayanan > 0 && t.IdPemohon > 0)
                throw new SystemException("Data Tidak Lengkap");
            return true;
        }

       
    }
}
