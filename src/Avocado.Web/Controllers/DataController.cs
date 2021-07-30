using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Avocado.Base.Interfaces;
using Avocado.Web.Helpers;
using Avocado.Web.Models;
using Avocado.Web.Models.Data;

namespace Avocado.Web.Controllers
{
    [ApiController]
    public class DataController : BaseController
    {
        public DataController(IService service) : base(service)
        {
        }

        [HttpPost("/meter-reading-uploads")]
        public async Task<JsonResult> MeterReadingUploadAsync(IFormFile file)
        {
            var (valid, message) = file.IsValidCSVFile();
            if (!valid)
                return JsonErrorMessage(message);

            var data = await file.GetBytesAsync();
            var (total, processed, errors) = await Service.MeterReadingFileProcessor.Process(data);

            var result = "SUCCESS";
            if (errors.Count > 0 && processed > 0)
                result = "PARTIAL-SUCCESS";
            else if (errors.Count > 0 && processed == 0)
                result = "FAIL";

            return Json(new GeneralJsonMessage<MeterReadingUploadResult>
            {
                Result = result,
                Detail = new()
                {
                    Total = total,
                    Processed = processed,
                    Errors = errors
                }
            });
        }
    }
}