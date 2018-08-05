using Ocph.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using AppCore.ModelDTO;
using Ocph.DAL.Provider.MySql;

namespace AppCore
{
    public class OcphDbContext : MySqlDbConnection, IOcphDbContext
    {
        private IRepository<permohonan> _permohonans;

        public OcphDbContext(string constring)
        {

            this.ConnectionString = constring;
        }

        public OcphDbContext()
        {
            //  this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ConnectionString = "Server=localhost;database=dbpertanahan;UID=root;password=;CharSet=utf8;Persist Security Info=True";
        }

        public virtual IRepository<pemohon> Pemohons { get { return new Repository<pemohon>(this); } }
        public virtual IRepository<bidang> Bidangs { get { return new Repository<bidang>(this); } }
        public virtual IRepository<kategorilayanan> Kategories { get { return new Repository<kategorilayanan>(this); } }
        public virtual IRepository<kelengkapan> Kelengkapans { get { return new Repository<kelengkapan>(this); } }
        public virtual IRepository<layanan> Layanans { get { return new Repository<layanan>(this); } }
        public virtual IRepository<permohonan> Permohonans {
            get { return _permohonans; }
            set { _permohonans = new Repository<permohonan>(this); }
        }
        public virtual IRepository<persyaratan> Persyaratans{ get { return new Repository<persyaratan>(this); } }
        public virtual IRepository<petugas> Petugas { get { return new Repository<petugas>(this); } }
        public virtual IRepository<petugasdetail> PetugasDetails{ get { return new Repository<petugasdetail>(this); } }
        public virtual IRepository<tahapan> Tahapans { get { return new Repository<tahapan>(this); } }
        public virtual IRepository<progress> Progress { get { return new Repository<progress>(this); } }
       public virtual IRepository<tahapanlayanan> TahapanLayanan { get { return new Repository<tahapanlayanan>(this); } }


    }
}
