using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avocado.Base.Interfaces;
using Avocado.Base.Interfaces.FileProcessors;
using Avocado.Tests.Shared;
using Avocado.Web.Controllers;
using Avocado.Web.Models;
using Avocado.Web.Models.Data;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using Xunit;

namespace Avocado.Web.Tests.Controllers
{
    public class DataControllerTests : BaseTest
    {
        [Fact]
        public async Task MeterReadingUploadAsync_Basic()
        {
            // arrange
            var controller = Mocker.CreateInstance<DataController>();

            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(1);
            moqFile.Setup(x => x.ContentType).Returns("text/csv");

            var moqMeterReadingFileProcessor = Mocker.GetMock<IMeterReadingFileProcessor>();
            moqMeterReadingFileProcessor.Setup(x => x.Process(It.IsAny<byte[]>()).Result)
                .Returns((1, 1, new List<string>()));

            Mocker.GetMock<IService>().Setup(x => x.MeterReadingFileProcessor).Returns(moqMeterReadingFileProcessor.Object);

            // act
            var result = await controller.MeterReadingUploadAsync(moqFile.Object);

            // assert
            result.Value.ShouldBeAssignableTo<GeneralJsonMessage<MeterReadingUploadResult>>().ShouldSatisfyAllConditions(
                x => x.Result.ShouldBe("SUCCESS"),
                x => x.Detail.ShouldSatisfyAllConditions(
                    c => c.Total.ShouldBe(1),
                    c => c.Processed.ShouldBe(1),
                    c => c.Errors.ShouldBeEmpty()));
        }

        [Fact]
        public async Task MeterReadingUploadAsync_Missing_File()
        {
            // arrange
            var controller = Mocker.CreateInstance<DataController>();

            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(0);

            // act
            var result = await controller.MeterReadingUploadAsync(moqFile.Object);

            // assert
            result.Value.ShouldBeAssignableTo<GeneralJsonMessage<string>>().ShouldSatisfyAllConditions(
                x => x.Result.ShouldBe("FAIL"));
        }

        [Fact]
        public async Task MeterReadingUploadAsync_Some_Errors()
        {
            // arrange
            var controller = Mocker.CreateInstance<DataController>();

            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(1);
            moqFile.Setup(x => x.ContentType).Returns("text/csv");

            var moqMeterReadingFileProcessor = Mocker.GetMock<IMeterReadingFileProcessor>();
            moqMeterReadingFileProcessor.Setup(x => x.Process(It.IsAny<byte[]>()).Result)
                .Returns((2, 1, new List<string> {"A nasty error"}));

            Mocker.GetMock<IService>().Setup(x => x.MeterReadingFileProcessor).Returns(moqMeterReadingFileProcessor.Object);

            // act
            var result = await controller.MeterReadingUploadAsync(moqFile.Object);

            // assert
            result.Value.ShouldBeAssignableTo<GeneralJsonMessage<MeterReadingUploadResult>>().ShouldSatisfyAllConditions(
                x => x.Result.ShouldBe("PARTIAL-SUCCESS"),
                x => x.Detail.ShouldSatisfyAllConditions(
                    c => c.Total.ShouldBe(2),
                    c => c.Processed.ShouldBe(1),
                    c => c.Errors.Count().ShouldBe(1),
                    c => c.Errors.First().ShouldBe("A nasty error")));
        }

        [Fact]
        public async Task MeterReadingUploadAsync_All_Errors()
        {
            // arrange
            var controller = Mocker.CreateInstance<DataController>();

            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(1);
            moqFile.Setup(x => x.ContentType).Returns("text/csv");

            var moqMeterReadingFileProcessor = Mocker.GetMock<IMeterReadingFileProcessor>();
            moqMeterReadingFileProcessor.Setup(x => x.Process(It.IsAny<byte[]>()).Result)
                .Returns((1, 0, new List<string> {"Everyone loves Avocados"}));

            Mocker.GetMock<IService>().Setup(x => x.MeterReadingFileProcessor).Returns(moqMeterReadingFileProcessor.Object);

            // act
            var result = await controller.MeterReadingUploadAsync(moqFile.Object);

            // assert
            result.Value.ShouldBeAssignableTo<GeneralJsonMessage<MeterReadingUploadResult>>().ShouldSatisfyAllConditions(
                x => x.Result.ShouldBe("FAIL"),
                x => x.Detail.ShouldSatisfyAllConditions(
                    c => c.Total.ShouldBe(1),
                    c => c.Processed.ShouldBe(0),
                    c => c.Errors.Count().ShouldBe(1),
                    c => c.Errors.First().ShouldBe("Everyone loves Avocados")));
        }
    }
}