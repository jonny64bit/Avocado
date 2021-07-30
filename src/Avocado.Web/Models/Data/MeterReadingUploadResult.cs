using System.Collections.Generic;

namespace Avocado.Web.Models.Data
{
    public class MeterReadingUploadResult
    {
        public int Processed { get; set; }
        public int Total { get; set; }
        public List<string> Errors { get; set; }
    }
}