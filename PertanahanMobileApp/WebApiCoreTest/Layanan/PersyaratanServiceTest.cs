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

namespace WebApiCoreTest.Layanan
{
   public class PersyaratanServiceTest
    {
        private MockDataContext context = new MockDataContext();
        private IPersyaratanUOW unitWork;
        private LayananService service;

        public PersyaratanServiceTest()
        {
            unitWork = A.Fake<IPersyaratanUOW>();
            service = new LayananService(unitWork);
        }

        [Fact]
        public void InsertTest()
        {
            //expected NotNull , actual NotNull
            persyaratan kat = new persyaratan();
            A.CallTo(() => unitWork.InsertPersyaratan(new persyaratan()))
                .WithAnyArguments()
                .Returns(new persyaratan());
            Assert.NotNull(service.InsertPersyaratan(kat));


            //when persyaratan Null ecpected Null, actual Null
            persyaratan itemnull = null;

            Assert.Throws<SystemException>(() => service.InsertPersyaratan(null));
        }

        [Fact]
        public void UpdateTest()
        {
            //expected NotNull , actual NotNull
            persyaratan kat = new persyaratan();
            A.CallTo(() => unitWork.UpdatePersyaratan(new persyaratan()))
                .WithAnyArguments()
                .Returns(new persyaratan());
            Assert.NotNull(service.UpdatePersyaratan(kat));

            //when persyaratan Null ecpected Null, actual Null
            persyaratan itemnull = null;

            Assert.Throws<SystemException>(() => service.UpdatePersyaratan(null));

        }

        [Fact]
        public void DeleteTest()
        {
            //expected NotNull , actual NotNull
            persyaratan kat = new persyaratan();
            A.CallTo(() => unitWork.DeletePersyaratan(1))
                .WithAnyArguments()
                .Returns(true);
            Assert.NotNull(service.DeletePersyaratan(1));

            //when Id persyaratan 0 ecpected Thrw, actual Trow
            Assert.Throws<SystemException>(() => service.DeletePersyaratan(0));
        }

        [Fact]
        public void SelectPersyaratans()
        {
            //expected NotEmpty actual NotEmpty
            A.CallTo(() => unitWork.GetPersyaratans(new layanan()))
                .WithAnyArguments()
                .Returns(new List<persyaratan>());
            Assert.NotNull(service.GetPersyaratans(new layanan()));

            //When layanan null expected = Throw actual throw

            Assert.Throws<SystemException>(() => service.GetPersyaratans(null));

        }



        [Fact]
        public void SelectPersyaratan()
        {
            //expected NotEmpty actual NotEmpty
            A.CallTo(() => unitWork.GetPersyartan(1))
                .WithAnyArguments()
                .Returns(new persyaratan());
            Assert.NotNull(service.GetPersyartan(1));

            //When layanan null expected = Throw actual throw

            Assert.Throws<SystemException>(() => service.GetPersyartan(0));
        }

    }
}
