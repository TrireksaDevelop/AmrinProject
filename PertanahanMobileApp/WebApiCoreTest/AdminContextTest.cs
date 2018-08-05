using AppCore.ModelDTO;
using AppCore.Services;
using AppCore.UnitOfWorks.InterfaceUnitOfWork;
using FakeItEasy;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace WebApiCoreTest
{
    public class AdminContextTest
    {
        private MockDataContext context;
        private IAdminService service;
        private IBidangUOW bidangUow;
        private IPermohonanService permohonanService;

        public AdminContextTest()
        {
            bidangUow = A.Fake<IBidangUOW>();
            A.CallTo(() => bidangUow.GetBidangTugas(new petugas())).WithAnyArguments().Returns(new bidang());
            permohonanService = A.Fake<IPermohonanService>();
            service = new AdminService(new AppCore.ModelDTO.petugas(), permohonanService, bidangUow);
        }


        [Fact]
        public void GetTahapanTugasAdmin()
        {
            //When BidangTugas NotNull expected NotNull actual NotNull
          
            A.CallTo(() => bidangUow.GetTahapanTugasBidang(new AppCore.ModelDTO.bidang()))
                .WithAnyArguments()
                .Returns(new List<AppCore.ModelDTO.tahapan>());

            Assert.NotNull(service.GetTahapans());

            //When BidangTugas Null expected Throw actual Trow
            service.BidangTugas = null;

            Assert.Throws<SystemException>(() => service.GetTahapans());
        }
        [Fact]
        public void GetPermohonanOnAdminBidang()
        {
            //When BidangTugas NotNull expected NotNull actual NotNull

            A.CallTo(() => bidangUow.GetAllPermohonan(new AppCore.ModelDTO.bidang()))
                .WithAnyArguments()
                .Returns(new List<permohonan>());

            Assert.NotNull(service.GetPermohonans());

            //When BidangTugas Null expected Throw actual Trow
            service.BidangTugas = null;

            Assert.Throws<SystemException>(() => service.GetPermohonans());
        }

        [Fact]

        public void GetKelengkapanOfPermohonan()
        {
            //When Permohonan NotNull expected NotNull actual NotNull

            A.CallTo(() => permohonanService.GetKelengkapan(new permohonan()))
                .WithAnyArguments()
                .Returns(new List<kelengkapan>());

            Assert.NotNull(service.GetKelengkapans(new permohonan()));

            //When Permohonan Null expected Throw actual Trow

            Assert.Throws<SystemException>(() => service.GetKelengkapans(null));
        }

        
        [Fact]
        public void ChangeWork()
        {
            //When Permohonan Null
            Assert.Throws<SystemException>(() => service.ChangeWork(null, new progress()));


            //When tahapan Null
            Assert.Throws<SystemException>(() => service.ChangeWork(new permohonan(),null));

            //When Not Success
            A.CallTo(() => bidangUow.ChangeWork(new permohonan(), new progress()))
                .WithAnyArguments().Returns(false);

            Assert.False(service.ChangeWork(new permohonan(), new progress()));

            A.CallTo(() => bidangUow.ChangeWork(new permohonan(), new progress()))
               .WithAnyArguments().Returns(true);

            Assert.True(service.ChangeWork(new permohonan(), new progress()));
        }


    }
}
