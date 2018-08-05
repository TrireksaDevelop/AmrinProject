using AppCore.ModelDTO;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace WebApiCoreTest.Layanan
{
    public class LayananSeviceUnitTest
    {
        private MockDataContext context;
        Mock<AppCore.Services.ILayananService> service = new Mock<AppCore.Services.ILayananService>();

        public LayananSeviceUnitTest()
        {
            this.context = new MockDataContext();
        }
        //Get

        [Fact]
        public void GetLayanan_HarapanTidakKosng_KenyataanNyaKosong()
        {
            service.Setup(O => O.GetLayanan()).Returns(context.Layanans);
            var result =service.Object.GetLayanan();
            Assert.True(result.Count > 0);

        }

        [Fact]
        public void GetTahapansFromLayananItem_HarapanTidakKosng_KenyataanNyaKosong()
        {
            var tah = new layanan();
            service.Setup(O => O.GetTahapans(tah)).Returns(new List<tahapan>());
            var result = service.Object.GetTahapans(tah);
            Assert.True(result.Count <= 0);
        }

        [Fact]
        public void GetTahapansFromLayananItem_HarapanTidakKosng_KenyataanTidakKosong()
        {
            var tah = new layanan();
            service.Setup(O => O.GetTahapans(tah)).Returns(new List<tahapan>() { new tahapan() });
            var result = service.Object.GetTahapans(tah);
            Assert.True(result.Count >0);
        }

        [Fact]
        public void GetThapansFromLayanan_HarapanThrowExp_ArgumenExeption()
        {
            service.Setup(O => O.GetTahapans(null)).Throws(new ArgumentNullException("layanan", "Data Tidak Ditemukan"));
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Object.GetTahapans(null));
            Assert.Equal(typeof(ArgumentNullException), ex.GetType());
        }


        //Get By Id
        [Fact]
        public void GetLayananById_Harapan_throwArgExp_Hasil_throwArgEx()
        {
            service.Setup(O => O.GetLayananById(0)).Throws(new ArgumentException("Tidak Boleh Kosong", "Id"));
            Exception ex = Assert.Throws<ArgumentException>(() => service.Object.GetLayananById(0));
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }


        [Fact]
        public void GetLayananById_HarapanNotNull_KenyataanNotNull()
        {
            service.Setup(O => O.GetLayananById(1)).Returns(new layanan());
            var result = service.Object.GetLayananById(1);
            Assert.True(result != null);
        }

        [Fact]
        public void GetLayananById_HarapanNull_KenyataanNull()
        {
            layanan tah = null;
            service.Setup(O => O.GetLayananById(1)).Returns(tah);
            var result = service.Object.GetLayananById(1);
            Assert.Equal(null, result);
        }

        //Delete

        [Fact]
        public void DeleteTahapan_HarapanThrowNullExp_ArgumenNullExeption()
        {
           
            service.Setup(O => O.DeleteLayanan(0)).Throws(new ArgumentException("layanan", "Data Tidak Ditemukan"));
            Exception ex = Assert.Throws<ArgumentException>(() => service.Object.DeleteLayanan(0));
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }

        [Fact]
        public void DeleteLayananItem_HarapanTrue_KenyataanTrue()
        {
            service.Setup(O => O.DeleteLayanan(1)).Returns(true);
            var result = service.Object.DeleteLayanan(1);
            Assert.Equal(true, result);
        }

        [Fact]
        public void DeleteLayananItem_HarapanFalse_KenyataanFalse()
        {
            service.Setup(O => O.DeleteLayanan(1)).Returns(false);
            var result = service.Object.DeleteLayanan(1);
            Assert.Equal(false, result);
        }
        //insert

        [Fact]
        public void InsertLayanan_Harapan_throwArgNullExp_Hasil_throwArgNullEx()
        {
            service.Setup(O => O.InsertLayanan(null)).Throws(new ArgumentNullException("layanan","IsNull"));
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Object.InsertLayanan(null));
            Assert.Equal(typeof(ArgumentNullException), ex.GetType());
        }


        [Fact]
        public void InsertLayananItem_HarapanNotNull_KenyataanNotNull()
        {
            var tah = new layanan();
            service.Setup(O => O.InsertLayanan(tah)).Returns(tah);
            var result = service.Object.InsertLayanan(tah);
            Assert.True(result != null);
        }

        [Fact]
        public void InsertLayananItem_HarapanNull_KenyataanNull()
        {
            layanan tah = null;
            service.Setup(O => O.InsertLayanan(tah)).Returns(tah);
            var result = service.Object.InsertLayanan(tah);
            Assert.Equal(null, result);
        }


        ///update
        [Fact]
        public void UpdateLayanan_Harapan_throwArgNullExp_Hasil_throwArgNullEx()
        {
            service.Setup(O => O.UpdateLayanan(null)).Throws(new ArgumentNullException("layanan", "IsNull"));
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Object.UpdateLayanan(null));
            Assert.Equal(typeof(ArgumentNullException), ex.GetType());
        }


        [Fact]
        public void UpdateLayananItem_HarapanNotNull_KenyataanNotNull()
        {
            var tah = new layanan();
            service.Setup(O => O.UpdateLayanan(tah)).Returns(tah);
            var result = service.Object.UpdateLayanan(tah);
            Assert.True(result != null);
        }

        [Fact]
        public void UpdateLayananItem_HarapanNull_KenyataanNull()
        {
            layanan tah = null;
            service.Setup(O => O.UpdateLayanan(null)).Returns(tah);
            var result = service.Object.UpdateLayanan(null);
            Assert.Equal(null, result);
        }
    }
}
