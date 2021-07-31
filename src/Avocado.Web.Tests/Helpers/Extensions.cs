using Avocado.Tests.Shared;
using Avocado.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Shouldly;
using Xunit;

namespace Avocado.Web.Tests.Helpers
{
    public class Extensions : BaseTest
    {
        [Fact]
        public void IsValidCSVFile_Basic()
        {
            // arrange
            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(1);
            moqFile.Setup(x => x.ContentType).Returns("text/csv");

            // act
            var result = moqFile.Object.IsValidCSVFile();

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.valid.ShouldBe(true),
                x => x.message.ShouldBe(null));
        }

        [Fact]
        public void IsValidCSVFile_Wrong_Content_Type()
        {
            // arrange
            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(1);
            moqFile.Setup(x => x.ContentType).Returns("text/html");

            // act
            var result = moqFile.Object.IsValidCSVFile();

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.valid.ShouldBe(false),
                x => x.message.ShouldBe("File is wrong content type"));
        }

        [Fact]
        public void IsValidCSVFile_Empty_File()
        {
            // arrange
            var moqFile = Mocker.GetMock<IFormFile>();
            moqFile.Setup(x => x.Length).Returns(0);
            moqFile.Setup(x => x.ContentType).Returns("text/csv");

            // act
            var result = moqFile.Object.IsValidCSVFile();

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.valid.ShouldBe(false),
                x => x.message.ShouldBe("File missing or empty in content"));
        }

        [Fact]
        public void IsValidCSVFile_Null_File()
        {
            // arrange

            // act
            var result = Avocado.Web.Helpers.Extensions.IsValidCSVFile(null);

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.valid.ShouldBe(false),
                x => x.message.ShouldBe("File missing or empty in content"));
        }
    }
}