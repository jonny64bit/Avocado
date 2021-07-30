using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avocado.Base.Interfaces.FileProcessors;
using Avocado.Database;
using Avocado.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Avocado.Base.Services.FileProcessors
{
    public class MeterReadingFileProcessor : IMeterReadingFileProcessor
    {
        private readonly DAL _context;

        public MeterReadingFileProcessor(DAL context)
        {
            _context = context;
        }

        readonly string[] _datetimeFormats = {"dd/MM/yyyy HH:mm"};

        public async Task<(int total, int processed, List<string> errors)> Process(byte[] data)
        {
            var rawContent = Encoding.Default.GetString(data);
            var newline = rawContent.Contains("\r\n") ? "\r\n" : "\n";
            var lines = rawContent.Split(newline).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            var processedMeterReadings = new List<ProcessedLine>();

            var firstLine = true;
            foreach (var line in lines)
            {
                //If they have sent a header row skip it.
                if (firstLine)
                {
                    firstLine = false;
                    if (line.Trim().ToLowerInvariant().StartsWith("account"))
                        continue;
                }

                var processedMeterReading = ParseLine(line);

                //Check we know of this account
                if (processedMeterReading.Valid && !await _context.Accounts.AnyAsync(x => x.Id == processedMeterReading.MeterReading.AccountId))
                    processedMeterReading.Error = "Unrecognized account Id.";

                processedMeterReadings.Add(processedMeterReading);
            }

            //Check for duplicates
            var duplicateGroups = processedMeterReadings.Where(x => x.Valid).GroupBy(x => new {x.MeterReading.Account, x.MeterReading.Value}, x => x)
                .Where(x => x.Count() > 1)
                .ToList();
            foreach (var duplicateMeterReading in duplicateGroups.SelectMany(duplicateGroup => duplicateGroup))
                duplicateMeterReading.Error = "Rejecting duplicate.";

            var validMeterReadings = processedMeterReadings.Where(x => x.Valid).Select(x => x.MeterReading).ToList();
            await _context.MeterReadings.AddRangeAsync(validMeterReadings);
            await _context.SaveChangesAsync();

            return (
                processedMeterReadings.Count,
                processedMeterReadings.Count(x => x.Valid),
                processedMeterReadings.Where(x => !x.Valid).Select(x => x.Error + " " + x.RawLine).ToList()
            );
        }

        private ProcessedLine ParseLine(string line)
        {
            var segments = line.Split(',');

            //Correct number of values
            if (segments.Length != 3)
                return new ProcessedLine(line, "Incorrect number of segments.");

            //Account Id
            if (!int.TryParse(segments[0], out var accountId))
                return new ProcessedLine(line, "Unable to parse account Id.");

            //When
            if (!DateTime.TryParseExact(segments[1], _datetimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var meterReadingDateTime))
                return new ProcessedLine(line, "Unable to parse meter reading date time.");

            //Value
            if (segments[2].Length != 5 || !segments[2].All(char.IsDigit) || !int.TryParse(segments[2], out var value))
                return new ProcessedLine(line, "Unable to parse meter reading value.");

            return new ProcessedLine(line, null, new()
            {
                AccountId = accountId,
                Value = value,
                When = meterReadingDateTime
            });
        }

        private class ProcessedLine
        {
            public ProcessedLine(string rawLine, string error, MeterReading meterReading = null)
            {
                RawLine = rawLine;
                Error = error;
                MeterReading = meterReading;
            }

            public string RawLine { get; }
            public bool Valid => string.IsNullOrWhiteSpace(Error);
            public string Error { get; set; }
            public MeterReading MeterReading { get; }
        }
    }
}