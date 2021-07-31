using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avocado.Base.Services.FileProcessors;
using Avocado.Tests.Shared;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Avocado.Base.Tests.Services.FileProcessors
{
    public class MeterReadingFileProcessorTests : BaseTest
    {
        private static byte[] FakeFileToBytes(string file) => Encoding.Default.GetBytes(file);
        readonly string _correctHeader = "AccountId,MeterReadingDateTime,MeterReadValue" + Environment.NewLine;

        [Fact]
        public async Task Basic()
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            var row = "12345,22/04/2019 12:25,45522";

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + row));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(1),
                x => x.errors.ShouldBeEmpty());

            (await Context.MeterReadings.CountAsync()).ShouldBe(1);
            (await Context.MeterReadings.FirstAsync()).ShouldSatisfyAllConditions(
                x => x.AccountId.ShouldBe(12345),
                x => x.Value.ShouldBe(45522),
                x => x.When.DateTime.ShouldBe(new DateTime(2019, 4, 22, 12, 25, 0)));
        }

        [Fact]
        public async Task Basic_No_Header_Row()
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            var row = "12345,22/04/2019 12:25,45522";

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(row));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(1),
                x => x.errors.ShouldBeEmpty());

            (await Context.MeterReadings.CountAsync()).ShouldBe(1);
            (await Context.MeterReadings.FirstAsync()).ShouldSatisfyAllConditions(
                x => x.AccountId.ShouldBe(12345),
                x => x.Value.ShouldBe(45522),
                x => x.When.DateTime.ShouldBe(new DateTime(2019, 4, 22, 12, 25, 0)));
        }

        [Theory]
        [InlineData("1241")]
        [InlineData("1241,11/04/2019 09:24")]
        [InlineData("1241,11/04/2019 09:24,00436,X")]
        public async Task Incorrect_Row_Segments(string line)
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + line));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(0),
                x => x.errors.Count.ShouldBe(1),
                x => x.errors.First().ShouldStartWith("Incorrect number of segments."));

            (await Context.MeterReadings.CountAsync()).ShouldBe(0);
        }

        [Fact]
        public async Task Incorrect_Account_Id()
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            var row = "FISH,22/04/2019 12:25,45522";

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + row));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(0),
                x => x.errors.Count.ShouldBe(1),
                x => x.errors.First().ShouldStartWith("Unable to parse account Id."));

            (await Context.MeterReadings.CountAsync()).ShouldBe(0);
        }

        [Fact]
        public async Task Incorrect_DateTime()
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            var row = "12345,04/22/2019 12:25,45522";

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + row));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(0),
                x => x.errors.Count.ShouldBe(1),
                x => x.errors.First().ShouldStartWith("Unable to parse meter reading date time."));

            (await Context.MeterReadings.CountAsync()).ShouldBe(0);
        }

        [Theory]
        [InlineData("2346,22/04/2019 12:25,999999")]
        [InlineData("2349,22/04/2019 12:25,VOID")]
        [InlineData("2344,08/05/2019 09:24,0X765")]
        [InlineData("6776,09/05/2019 09:24,-06575")]
        [InlineData("4534,11/05/2019 09:24,")]
        public async Task Incorrect_Values(string line)
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + line));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(0),
                x => x.errors.Count.ShouldBe(1),
                x => x.errors.First().ShouldStartWith("Unable to parse meter reading value."));

            (await Context.MeterReadings.CountAsync()).ShouldBe(0);
        }

        [Fact]
        public async Task Unknown_Account()
        {
            // arrange
            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            var row = "12345,22/04/2019 12:25,45522";

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + row));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(1),
                x => x.processed.ShouldBe(0),
                x => x.errors.Count.ShouldBe(1),
                x => x.errors.First().ShouldStartWith("Unrecognized account Id."));

            (await Context.MeterReadings.CountAsync()).ShouldBe(0);
        }

        [Fact]
        public async Task Reject_Duplicate()
        {
            // arrange
            await Context.Accounts.AddAsync(new() {Id = 12345, FirstName = "James", LastName = "Bond"});
            await Context.SaveChangesAsync();

            var fileProcessor = Mocker.CreateInstance<MeterReadingFileProcessor>();

            var row = "12345,22/04/2019 12:25,45522";

            // act
            var result = await fileProcessor.Process(FakeFileToBytes(_correctHeader + row + Environment.NewLine + row));

            // assert
            result.ShouldSatisfyAllConditions(
                x => x.total.ShouldBe(2),
                x => x.processed.ShouldBe(0),
                x => x.errors.ShouldAllBe(c => c.StartsWith("Rejecting duplicate.")));

            (await Context.MeterReadings.CountAsync()).ShouldBe(0);
        }
    }
}