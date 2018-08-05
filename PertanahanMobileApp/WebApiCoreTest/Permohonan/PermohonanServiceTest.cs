
using Moq;
using System;
using Xunit;
using AppCore.ModelDTO;
using AppCore.Services;
using AppCore.UnitOfWorks;
using FakeItEasy;
using FakeItEasy.Sdk;
using System.Collections.Generic;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;

namespace WebApiCoreTest.Permohonan
{

    ///Author : Ocph23
    ///Module : Mock Permohonan service
    ///

    public class PermohonanServiceTest
    {
        private PermohonanService service;
        private IPermohonanUOW unitWork;

        public PermohonanServiceTest()
        {
           
             unitWork = A.Fake<IPermohonanUOW>();
            A.CallTo(() => unitWork.GetDaftarPermohonan(new pemohon())).WithAnyArguments()
              .Returns(new List<permohonan>() { new permohonan()});
            A.CallTo(() => unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(
                 new List<progress>()
                 {
                    new progress{ Id=1},
                    new progress{Id=2}
                 });
            service = A.Fake<PermohonanService>((O)=>
            {
                O.Implements<IPermohonanService>();
                O.WithArgumentsForConstructor(new object[] {new pemohon(),unitWork });
               
              
            });
            //service.UnitWorkPermohonan = unitWork;
        }

        [Fact]
        public void CreateTestValidate()
        {
            //TestParameter Layanan IsNull, expected ThrowArgumentNull , actual ThrowargumenNull

            IPermohonanService newservice = new PermohonanService(new pemohon { Id = 1 },unitWork);
            Assert.Throws<ArgumentNullException>(() => newservice.CreatePermohonan(null));


            //TestParameter Pemohon IsNull , expected ThrowArgumentNull, actual ThrowargumentNull
            newservice = new PermohonanService(null,unitWork);
            Assert.Throws<ArgumentNullException>(() => newservice.CreatePermohonan(new layanan()));
        }

        [Fact]
        public void CreatePermohonan_expected_Null_actual_Null()
        {
            var t = new layanan { Id = 1, IdKategoriLayanan = 1 };
            var itemPermohonan = new permohonan { IdLayanan = t.Id, IdPemohon = 1, Status = AppCore.StatusPermohonan.Baru };
           
            A.CallTo(() => (unitWork.InsertNewPermohonan(itemPermohonan, t))).WithAnyArguments().Returns(null);

            Assert.Throws<ArgumentNullException>(() => service.CreatePermohonan(t));


        }

        [Fact]
        public void CreatePermohonan_expected_true_actual_true()
        {
            var t = new layanan { Id = 1, IdKategoriLayanan = 1 };
            var itemPermohonan = new permohonan { IdLayanan = t.Id, IdPemohon = 1, Status = AppCore.StatusPermohonan.Baru };
            A.CallTo(() => (unitWork.InsertNewPermohonan(itemPermohonan, t))).WithAnyArguments().Returns(new permohonan() { Id = 25 });
            var result = service.CreatePermohonan(t);
            Assert.True(result);

        }


        [Fact]
        public void GetItemTahapans_When_Permohonan_IsNull_expected_throwExpt_actual_throwExp()
        {
            service.SetCurrentPermohonan(null);
            Assert.Throws<SystemException>(() => service.ItemsTahapan());
        }

        [Fact]
        public void GetItemTahapans_When_Permohonan_IsNotNull_expected_NotNull_actual_NutNull()
        {
            A.CallTo(() => unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(new System.Collections.Generic.List<progress>());
            service.SetCurrentPermohonan(new permohonan());
            Assert.NotNull(service.ItemsTahapan());

        }
        [Fact]
        public void GetCurrentTahapan_When_CurrentTahapan_Isnull_expected_ThrowSystemExc_actual_throwSystemExc()
        {
            service.SetCurrentPermohonan(new permohonan());
            Assert.Throws<SystemException>(() => service.GetCurrentTahapan());

        }

        [Fact]
        public void GetCurrenttahapan_When_CurrentTahapan_NotNull_expected_NotNull_actual_NotNull()
        {
          
            service.SetCurrentPermohonan(new permohonan());
            service.SetCurrentTahapan(new progress());
            Assert.NotNull(service.GetCurrentTahapan());
        }
        

        [Fact]
        public void GettLastahapan_when_listOfTahapan_Isnull_expected_NotNull_actual_NotNull()
        {
            service.SetCurrentPermohonan(null);
            A.CallTo(() => unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(
                new List<progress>()
                {
                    new progress{ Id=1},
                    new progress{Id=2}
                });
            service.SetCurrentPermohonan(new permohonan());
            Assert.NotNull(service.GetLastTahapan());
        }

        [Fact]
        public void GettLastahapan_when_listOfTahapangradeThen_0_expected_2_actual_2()
        {
            A.CallTo(() =>unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(
                new List<progress>()
                {
                    new progress{ Id=1},
                    new progress{Id=2}
                } );
            service.SetCurrentPermohonan(new permohonan());
            var result = service.GetLastTahapan();
            Assert.Equal(2,result.Id);
        }

        [Fact]
        public void GetNextTahapanWhen_CurrentTahapan_IsNull_expected_Throw_Actual_throw()
        {
          Assert.Throws<SystemException>(()=> service.GetNextTahapan());
        }

        [Fact]
        public void GetNextTahapanWhen_listOfTahapan_IsNull_expected_Throw_Actual_throw()
        {
            service.SetCurrentPermohonan(null);
            Assert.Throws<SystemException>(() => service.GetNextTahapan());
        }

        [Fact]
        public void GetNextTahapanWhen_listOfTahapan_0_expected_Throw_Actual_throw()
        {
            A.CallTo(() => unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(
                new List<progress>());
            service.SetCurrentPermohonan(new permohonan());
            service.ItemsTahapan();
            service.SetCurrentTahapan(new progress());
           
            Assert.Throws<SystemException>(() => service.GetNextTahapan());
        }

        [Fact]
        public void GetNextTahapanWhen_CurrentTahapan_Equal_Last_expected_Throw_Actual_throw()
        {
            A.CallTo(() => unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(
               new List<progress>()
               {
                    new progress{ Id=1},
                    new progress{Id=2}
               });
            service.SetCurrentPermohonan(new permohonan());
            service.ItemsTahapan();
            service.SetCurrentTahapan(new progress() { Id = 2 });

            Assert.Throws<SystemException>(() => service.GetNextTahapan());
        }

        [Fact]
        public void GetNextTahapan__expected_NotNull_Actual_NotNull()
        {
            A.CallTo(() => unitWork.GetItemsTahapan(new permohonan())).WithAnyArguments().Returns(
               new List<progress>()
               {
                    new progress{ Id=1},
                    new progress{Id=2}
               });
            service.SetCurrentPermohonan(new permohonan());
            service.ItemsTahapan();
            service.SetCurrentTahapan(new progress() { Id = 1 });

            Assert.Equal(2,service.GetNextTahapan().Id);
        }


        [Fact]
        public void GetDaftarPermohonan_expect_NotNull_actual_NotNull()
        {
            A.CallTo(() => unitWork.GetDaftarPermohonan(new pemohon()))
            .WithAnyArguments().Returns(
               new List<permohonan>());
            Assert.NotNull(service.GetPermohonans());

        }



    }
}
