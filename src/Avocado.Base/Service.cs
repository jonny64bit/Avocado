using AutoMapper;
using Avocado.Base.Interfaces;
using Avocado.Base.Interfaces.FileProcessors;
using Avocado.Database;
using Microsoft.Extensions.Logging;

namespace Avocado.Base
{
    public class Service : IService
    {
        public Service(DAL context, ILoggerFactory loggerFactory, IMapper mapper, IMeterReadingFileProcessor meterReadingFileProcessor)
        {
            Context = context;
            Logger = loggerFactory.CreateLogger(GetType().Name);
            LoggerFactory = loggerFactory;
            Mapper = mapper;
            MeterReadingFileProcessor = meterReadingFileProcessor;
        }

        //NOTE: I would generally have more a hierarchy in here but seems overkill for this example.
        //Example:
        //IService
        //--->IFileProcessors
        //------->IMeterReadingFileProcessor
        //------->IBillingFileProcessor
        //--->ICacheService
        // etc. etc.

        public DAL Context { get; }
        public ILogger Logger { get; }
        public ILoggerFactory LoggerFactory { get; }
        public IMapper Mapper { get; }
        public IMeterReadingFileProcessor MeterReadingFileProcessor { get; }
    }
}