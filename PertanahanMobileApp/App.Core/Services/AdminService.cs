﻿using AppCore.ModelDTO;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public class AdminService : IAdminService
    {
        private petugas Petugas { get; }
        public IPermohonanService PermohonanService { get; set; }
        public IBidangUOW BidangUnitOfWork { get; set; }
        public List<bidang> ListBidangTugas { get; }
        public bidang BidangTugas { get; set; }

        public AdminService( petugas admin,IPermohonanService permohonanService,IBidangUOW bidangUow)
        {
            this.Petugas = admin;
            PermohonanService = permohonanService;
            BidangUnitOfWork = bidangUow;
            ListBidangTugas = BidangUnitOfWork.GetBidangTugas();
            
        }

        public permohonan GetPermohonanById(int id)
        {
            var result = PermohonanService.GetPermohonan(id);
            return result;
        }


        public List<tahapan> GetTahapans()
        {
            if (BidangTugas == null)
                throw new SystemException("Anda Tidak Memiliki Bidang tugas");
            else
            {
                return BidangUnitOfWork.GetTahapanTugasBidang(BidangTugas);
            }
        }


        public List<permohonan> GetPermohonans()
        {
            if (BidangTugas == null)
                throw new SystemException("Anda Tidak Memiliki Bidang tugas");
            else
              return BidangUnitOfWork.GetAllPermohonan(BidangTugas);
        }

        public List<kelengkapan> GetKelengkapans(permohonan item)
        {
            if(item==null)
                throw new SystemException("Item Permohonan Tidak Ada");
            else
                return PermohonanService.GetKelengkapan(item);
        }

        public bool ChangeWork(permohonan permohonan, progress tahapan)
        {
           if(permohonan==null || tahapan==null)
            {
                throw new SystemException("permohonan atau tahapan tidak ada");
            }else
            {
               return BidangUnitOfWork.ChangeWork(permohonan, tahapan);
            }
        }

        public void SendMessageToPemohon(string message)
        {
            throw new NotImplementedException();
        }

        public void SetBidangTugas(bidang bidang)
        {
            BidangTugas = bidang;
        }
    }
}
