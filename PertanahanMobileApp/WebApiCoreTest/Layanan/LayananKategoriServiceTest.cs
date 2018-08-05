using AppCore.ModelDTO;
using AppCore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebApiCoreTest.Layanan
{
   public class LayananKategoriServiceTest
    {
        private Mock<ILayananService> service;
        private MockDataContext data;

        public LayananKategoriServiceTest()
        {
            service =new Mock<ILayananService>();
            data = new MockDataContext();
        }

        [Fact]
        public void SelectKategories()
        {
            //expected NotEmpty actual NotEmpty
            service.Setup(O => O.GetKategories()).Returns(data.Kategories);
            var result = service.Object.GetKategories();
            Assert.True(result.Count > 0);


            //expected Empty actual empty
            service.Setup(O => O.GetKategories()).Returns(new List<kategorilayanan>());
            result = service.Object.GetKategories();
            Assert.True(result.Count <= 0);


        }



        [Fact]
        public void SelectKategory()
        {
            kategorilayanan kat = new kategorilayanan();
            //expected NotNull actual NotNull
            service.Setup(O => O.GetKategory(1)).Returns(kat);
            var result = service.Object.GetKategory(1);
            Assert.Equal(kat,result);


            //expected null actual null
            kategorilayanan katnull = null;
            service.Setup(O => O.GetKategory(1)).Returns(katnull);
            result = service.Object.GetKategory(1);
            Assert.Equal(null, result);


            service.Setup(O => O.GetKategory(0)).Throws(new ArgumentNullException("Id", "KategoriLayanan"));
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Object.GetKategory(0));
            Assert.Equal(typeof(ArgumentNullException), ex.GetType());
        }

        [Fact]
        public void InsertKategori_HarapanNotNull_NotNull()
        {
            kategorilayanan kat = new kategorilayanan();
            service.Setup(O => O.InsertKategory(kat)).Returns(kat);
            var result = service.Object.InsertKategory(kat);
            Assert.Equal(kat , result);

            kategorilayanan katnull = null;
            service.Setup(O => O.InsertKategory(kat)).Returns(katnull);
            result = service.Object.InsertKategory(kat);
            Assert.Equal(null, result);

            service.Setup(O => O.InsertKategory(null)).Throws(new ArgumentNullException("Kategori", "KategoriLayanan"));
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Object.InsertKategory(null));
            Assert.Equal(typeof(ArgumentNullException), ex.GetType());
        }

        [Fact]
        public void UpdateKategori()
        {
            //Expeted NotNull , actual notnull
            kategorilayanan kat = new kategorilayanan();
            service.Setup(O => O.UpdateKategory(kat)).Returns(kat);
            var result = service.Object.UpdateKategory(kat);
            Assert.Equal(kat, result);


            // expected null, actual null
            kategorilayanan katnull = null;
            service.Setup(O => O.UpdateKategory(kat)).Returns(katnull);
            result = service.Object.UpdateKategory(kat);
            Assert.Equal(null, result);


            //expected throw argumentNullEx actual argumentNullExc
            service.Setup(O => O.UpdateKategory(null)).Throws(new ArgumentNullException("Kategori", "KategoriLayanan"));
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.Object.UpdateKategory(null));
            Assert.Equal(typeof(ArgumentNullException), ex.GetType());


        }

        [Fact]
        public void DeletteKategori()
        {
            //Expeted true, actual true
            kategorilayanan kat = new kategorilayanan();
            service.Setup(O => O.DeleteKategory(1)).Returns(true);
            var result = service.Object.DeleteKategory(1);
            Assert.Equal(true, result);


            // expected null, actual null
            kategorilayanan katnull = null;
            service.Setup(O => O.DeleteKategory(1)).Returns(false);
            result = service.Object.DeleteKategory(1);
            Assert.Equal(false, result);


            //expected throw argumentNullEx actual argumentNullExc
            service.Setup(O => O.DeleteKategory(0)).Throws(new ArgumentException("Kategori", "KategoriLayanan"));
            Exception ex = Assert.Throws<ArgumentException>(() => service.Object.DeleteKategory(0));
            Assert.Equal(typeof(ArgumentException), ex.GetType());
        }




    }
}
