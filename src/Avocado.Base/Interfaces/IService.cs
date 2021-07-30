using AutoMapper;
using Avocado.Base.Interfaces.FileProcessors;
using Avocado.Database;
using Microsoft.Extensions.Logging;

namespace Avocado.Base.Interfaces
{
    public interface IService
    {
        DAL Context { get; }
        ILogger Logger { get; }
        ILoggerFactory LoggerFactory { get; }
        IMapper Mapper { get; }
        IMeterReadingFileProcessor MeterReadingFileProcessor { get; }
    }
}