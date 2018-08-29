using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCore.Services
{
    public class ClientService
    {

        public ClientService()
        {

        }

        public bool CreatePemohon(pemohon item)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Pemohons.Insert(item);
                    return result;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public ClientService(string userId)
        {
            Pemohon = GetPemohonBy(userId);
        }

        public pemohon GetPemohonBy(string userId)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Pemohons.Where(O => O.UserId == userId).FirstOrDefault();
                if (result != null)
                    return result;
                else
                    throw new SystemException("Anda Tidak Memiliki Akses");
            }
        }

        public List<permohonan> GetPermohonans()
        {
            throw new NotImplementedException();
        }

        public pemohon Pemohon { get; }

        public permohonan GetLastPermohonan()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Permohonans.Where(O => O.IdPemohon == Pemohon.Id).FirstOrDefault();
                if(result !=null)
                {
                    result.Persyaratan = db.Kelengkapans.Where(O => O.IdPermohonan == result.Id).ToList();
                    result.Tahapans = db.Progress.Where(O => O.IdPermohonan == result.Id).ToList();
                    result.Layanan = db.Layanans.Where(O => O.Id == result.IdLayanan).FirstOrDefault();
                }

                return result;
                         
            }
        }
    }
}
