﻿using AppCore.ModelDTO;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore.UnitOfWorks
{
    public class BidangUOW : IBidangUOW
    {
        public BidangUOW(petugas petugas)
        {
            SelectedPetugas = petugas;
        }

        public petugas SelectedPetugas { get; }

        public bool ChangeWork(permohonan permohonan, progress tahapan)
        {
            throw new NotImplementedException();
        }

        public List<permohonan> GetAllPermohonan(bidang bidangTugas)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var list = new List<permohonan>();

                    var permohonans = (from a in db.Tahapans.Where(O=>O.BidangId==bidangTugas.Id)
                                   join b in db.TahapanLayanan.Select() on a.Id equals b.TahapanId
                                   join c in db.Layanans.Select() on b.Id equals c.Id
                                   join d in db.Permohonans.Select() on c.Id equals d.IdLayanan
                                   join f in db.Pemohons.Select() on d.IdPemohon equals f.Id
                                   select new permohonan { Id=d.Id, IdLayanan=d.IdLayanan, IdPemohon=d.IdPemohon, Status=d.Status, Layanan=c ,Pemohon=f }).ToList();

                    var resutl = permohonans.GroupBy(O => O.Id);
                    foreach(var item in resutl)
                    {
                        var a =item.FirstOrDefault();
                        list.Add(a);
                    }
                    return list;
                }
            }
            catch (Exception)
            {

                return new List<permohonan>();
            }
        }


        public List<bidang> GetBidangTugas()
        {
            try
            {
                if (SelectedPetugas != null)
                {
                    using (var db = new OcphDbContext())
                    {
                        var bidangs = db.Bidangs.Where(O => O.PetugasId == SelectedPetugas.Id).ToList();
                        return bidangs;
                    }
                }
                else
                    throw new SystemException("Anda Tidak Memiliki akses");
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public List<tahapan> GetTahapanTugasBidang(bidang bidangTugas)
        {
            throw new NotImplementedException();
        }
    }
}
