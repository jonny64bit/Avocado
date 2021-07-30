using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avocado.Base.Interfaces.FileProcessors
{
    public interface IMeterReadingFileProcessor
    {
        Task<(int total, int processed, List<string> errors)> Process(byte[] data);
    }
}