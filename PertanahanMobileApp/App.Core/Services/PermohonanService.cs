﻿using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.ModelDTO;
using AppCore.UnitOfWorks;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;

namespace AppCore.Services
{
    public class PermohonanService : IPermohonanService
    {
        private List<progress> _tahapans;

        private progress _currentTahapan;

        public IPermohonanUOW UnitWorkPermohonan { get; set; }

        public pemohon Pemohon { get; private set; }

        public permohonan Permohonan { get; private set; }

        public PermohonanService(pemohon t,IPermohonanUOW uow)
        {
            Pemohon = t;
            UnitWorkPermohonan = uow;
            Permohonan = UnitWorkPermohonan.GetDaftarPermohonan(t).Last();
            _tahapans = UnitWorkPermohonan.GetItemsTahapan(Permohonan);
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

        public void SetCurrentTahapan(progress item)
        {
            _currentTahapan = item;
        }

        public progress GetCurrentTahapan()
        {
            if (_currentTahapan == null)
                throw new SystemException("Tahapan Belum Dipilih");
            else
             return _currentTahapan;
        }

        public progress GetLastTahapan()
        {
            if(_tahapans==null)
                _tahapans = UnitWorkPermohonan.GetItemsTahapan(Permohonan);

            if (_tahapans.Count<=0)
            {
                throw new SystemException("Data Tahapan Tidak Ada");
            }else 
            {
                return _tahapans.Last();
            }
        }

        public progress GetNextTahapan()
        {
           if(_currentTahapan!=null && _tahapans!=null)
            {
                if(_tahapans.Count<=0)
                    throw new SystemException("Data Tahapan Tidak Ada");
                else if (_currentTahapan.Id==_tahapans.Last().Id)
                    throw new SystemException("Sudah Di Tahapan Akhir");
                else
                {

                    return _tahapans.Where(O => O.Id == _currentTahapan.Id + 1).FirstOrDefault();
                }
            }
            else
            {
                throw new SystemException("Tahapan Awal Belum Dipilih");
            }

        }

        
        public bool CreatePermohonan(layanan t)
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
                    return true;
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
    }
}