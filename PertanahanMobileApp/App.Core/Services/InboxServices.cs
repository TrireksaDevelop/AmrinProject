using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppCore.Services
{
   public class InboxServices
    {
        public int PermohonanId { get; }

        public InboxServices(int permohonanId)
        {
            this.PermohonanId = permohonanId;
        }

        public List<inbox> GetPesans()
        {
            using (var db = new OcphDbContext())
            {
                var list = new List<inbox>();
                var petugases = from a in db.Inboxs.Where(O => O.PermohonanId == PermohonanId)
                             join c in db.Petugas.Select()  on a.UserId equals c.UserId
                             select  new inbox { Id=a.Id, Message=a.Message, PermohonanId=a.PermohonanId, Tanggal=a.Tanggal, UserId=a.UserId, UserName=c.Nama};
                foreach(var item in petugases)
                {
                    list.Add(item);
                }

                var cust = from a in db.Inboxs.Where(O => O.PermohonanId == PermohonanId)
                                join c in db.Pemohons.Select() on a.UserId equals c.UserId
                                select new inbox { Id = a.Id, Message = a.Message, PermohonanId = a.PermohonanId, Tanggal = a.Tanggal, UserId = a.UserId, UserName = c.Nama };

                foreach(var item in cust)
                {
                    list.Add(item);
                }


                return list;
            }

        }

        public bool AddNewMessage(inbox message)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    if (db.Inboxs.Insert(message))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


    }
}
