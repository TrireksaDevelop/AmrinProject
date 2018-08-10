using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AppCore.ModelDTO;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class MasterService : IMasterService
    {
        public bool DeleteBidang(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (db.Bidangs.Delete(O => O.Id == id))
                        return true;
                    return false;
                }
                catch
                {
                    throw new SystemException("Data Tidak Terhapus");
                }
            }
        }

        public bool DeletePetugas(int id)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if (db.Petugas.Delete(O => O.Id == id))
                        return true;
                    return false;
                }
                catch 
                {
                    throw new SystemException("Data Tidak Terhapus");
                }
            }
        }

        public bool DeleteTahapan(int item)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if(!db.Tahapans.Delete(O=>O.Id==item))
                    {
                        throw new SystemException("Data Tidak Berhasil Dihapus");
                    }

                    return true;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public List<bidang> GetBidang()
        {
            using (var db = new OcphDbContext())
            {

                try
                {
                    var results = from a in db.Bidangs.Select()
                                  join p in db.Petugas.Select() on a.PetugasId equals p.Id
                                  join c in db.Tahapans.Select() on a.Id equals c.BidangId into cGroup
                                  from c in cGroup.DefaultIfEmpty()
                                  select new bidang
                                  {
                                      Id = a.Id,
                                      Nama = a.Nama,
                                      Petugas = p,
                                      Descripsi = a.Descripsi,
                                      PetugasId = a.PetugasId, Tahapans=cGroup.ToList()
                                  };

                   

                    return results.ToList();
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }

            }
        }

        public bidang GetBidangById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var results = from a in db.Bidangs.Select()
                                  join p in db.Petugas.Select() on a.PetugasId equals p.Id
                                  join c in db.Tahapans.Select() on a.Id equals c.BidangId into cGroup
                                  from c in cGroup.DefaultIfEmpty()
                                  select new bidang
                                  {
                                      Id = a.Id,
                                      Nama = a.Nama,
                                      Petugas = p,
                                      Descripsi = a.Descripsi,
                                      PetugasId = a.PetugasId,
                                      Tahapans = cGroup.ToList()
                                  };


                    if (results.Count() <= 0)
                        throw new SystemException("Data Tidak Ditemukan");

                    return results.FirstOrDefault();
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public List<petugas> GetPetugas()
        {
            using (var db = new OcphDbContext())
            {
                try
                {

                    var results = from a in db.Petugas.Select()
                                  select a;
                    foreach(var item in results.ToList())
                    {
                        item.Bidangs = db.Bidangs.Where(O => O.PetugasId == item.Id);
                    }

                    return results.ToList();
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }

            }
        }

        public petugas GetPetugasById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var results = from a in db.Petugas.Where(O=>O.Id==id)
                                  join b in db.Bidangs.Select() on a.Id equals b.PetugasId into bGroup
                                  from b in bGroup.DefaultIfEmpty()
                                  select new petugas
                                  {
                                      Alamat = a.Alamat,
                                      Id = b.Id,
                                      Jabatan = a.Jabatan,
                                      Nama = a.Nama,
                                      NIP = a.NIP,
                                      Email = a.Email,
                                      UserId = a.UserId,
                                      Bidangs = bGroup
                                  };

                    if (results.Count() <= 0)
                        throw new SystemException("Data Tidak Ditemukan");
                    return results.FirstOrDefault();
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }

            }
        }

        public Task<petugas> GetPetugasByUserId(string userid)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var results = from a in db.Petugas.Where(O => O.UserId == userid)
                                  join b in db.Bidangs.Select() on a.Id equals b.PetugasId into bGroup
                                  from b in bGroup.DefaultIfEmpty()
                                  select new petugas
                                  {
                                      Alamat = a.Alamat,
                                      Id = b.Id,
                                      Jabatan = a.Jabatan,
                                      Nama = a.Nama,
                                      NIP = a.NIP, Email=a.Email,
                                      UserId = a.UserId,
                                      Bidangs = bGroup
                                  };

                    if (results.Count() <= 0)
                        throw new SystemException("Data Tidak Ditemukan");
                    return Task.FromResult(results.FirstOrDefault());
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }

            }
        }

        public List<tahapan> GetTahapan()
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    var results = from a in db.Tahapans.Select()
                                  join b in db.Bidangs.Select() on a.BidangId equals b.Id
                                  select new tahapan
                                  {
                                      BidangId = a.BidangId,
                                      Id = a.Id,
                                      Keterangan = a.Keterangan,
                                      Nama = a.Nama,
                                      Bidang = b
                                  };
                    if (results.Count() <= 0)
                        throw new SystemException("Data Tahapan Tidak Ditemukan");
                    return results.ToList();
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public tahapan GetTahapanById(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var results = from a in db.Tahapans.Where(O=>O.Id==id)
                                  join b in db.Bidangs.Select() on a.BidangId equals b.Id
                                  select new tahapan
                                  {
                                      BidangId = a.BidangId,
                                      Id = a.Id,
                                      Keterangan = a.Keterangan,
                                      Nama = a.Nama,
                                      Bidang = b
                                  };
                    if (results.Count() <= 0)
                        throw new SystemException("Data Tahapan Tidak Ditemukan");
                    return results.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }
            }
        }

        public petugas SaveChange(petugas pet)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if(pet.Id<=0)
                    {
                        pet.Id=db.Petugas.InsertAndGetLastID(pet);
                        if (pet.Id <= 0)
                            throw new SystemException("Data tidak tersimpan");
                    }else
                    {
                        if (db.Petugas.Update(O => new { O.Alamat, O.Jabatan, O.Nama, O.NIP }, pet, O => O.Id == pet.Id))
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }
                       
                    }
                    return pet;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public bidang SaveChange(bidang item)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (item.Id <= 0)
                    {
                        item.Id = db.Bidangs.InsertAndGetLastID(item);
                        if (item.Id <= 0)
                            throw new SystemException("Data tidak tersimpan");
                    }
                    else
                    {
                        if (!db.Bidangs.Update(O => new {O.Nama,O.PetugasId,O.Descripsi}, item, O => O.Id == item.Id))
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }

                    }
                    return item;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public tahapan SaveChange(tahapan item)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (item.Id <= 0)
                    {
                        item.Id = db.Tahapans.InsertAndGetLastID(item);
                        if (item.Id <= 0)
                            throw new SystemException("Data tidak tersimpan");
                    }
                    else
                    {
                        if (!db.Tahapans.Update(O => new { O.Nama, O.BidangId, O.Keterangan}, item, O => O.Id == item.Id))
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }

                    }
                    return item;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }
    }
}
