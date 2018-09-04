using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    result.Kelengkapans = db.Kelengkapans.Where(O => O.IdPermohonan == result.Id).ToList();
                    result.Tahapans = db.Progress.Where(O => O.IdPermohonan == result.Id).ToList();
                    result.Layanan = db.Layanans.Where(O => O.Id == result.IdLayanan).FirstOrDefault();
                }

                return result;
                         
            }
        }

        public Task<pemohon> UpdateProfile(pemohon value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (!db.Pemohons.Update(O => new { O.Alamat, O.Foto, O.Nama, O.NIK }, value, O => O.Id == value.Id))
                        throw new SystemException("Data Tidak Tersimpan");
                    return Task.FromResult(value);
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }

            }
        }
    }
}
