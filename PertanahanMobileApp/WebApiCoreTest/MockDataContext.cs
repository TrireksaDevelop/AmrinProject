using AppCore.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiCoreTest
{
  public  class MockDataContext
    {
        public MockDataContext()
        {
            this.Permohonans = new List<permohonan>() {
                new permohonan{ Id=1, IdLayanan=1, IdPemohon=1,  Status= AppCore.StatusPermohonan.Baru},
                  new permohonan{ Id=2, IdLayanan=1, IdPemohon=1,  Status= AppCore.StatusPermohonan.Pengerjaan}
            };

            this.Pemohons = new List<pemohon>()
            {
                new pemohon{ Alamat="Jln.", Id=1, Nama="Ajenk", NIK="12131231", UserId="1"}
            };

            this.Kategories = new List<kategorilayanan>() {
                new kategorilayanan{ Id=1, Nama="Pemisahan"}
            };

            this.Layanans = new List<layanan>() { new layanan { Id = 0, IdKategoriLayanan = 1, Nama = "Bagi Dua" } };

            this.Persyaratans = new List<persyaratan>() {
                new persyaratan{ Id=1, IdLayanan=1, Nama="KTP"},
                  new persyaratan{ Id=2, IdLayanan=1, Nama="Kartu Keluarga"}
            };
        }

        public List<permohonan> Permohonans { get; }
        public List<pemohon> Pemohons { get; }
        public List<kategorilayanan> Kategories { get; }
        public List<layanan> Layanans { get; }

        public List<persyaratan> Persyaratans { get; }
    }
}
