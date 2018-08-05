using AppCore.ModelDTO;
using Ocph.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore
{
    public interface IOcphDbContext
    {
        IRepository<pemohon> Pemohons { get; }
        IRepository<bidang> Bidangs { get; }
        IRepository<kategorilayanan> Kategories
        { get; }
        IRepository<kelengkapan> Kelengkapans { get; }
        IRepository<layanan> Layanans { get; }
        IRepository<permohonan> Permohonans { get; set; }
        IRepository<persyaratan> Persyaratans { get; }
        IRepository<petugas> Petugas { get; }
        IRepository<petugasdetail> PetugasDetails { get; }
        IRepository<tahapan> Tahapans { get; }
        IRepository<progress> Progress { get; }

    }
}
