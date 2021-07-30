using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Avocado.Web.Helpers
{
    public static class Extensions
    {
        //NOTE: Generally this sort of function would go in a shared Nuget so it can be easily used across lots of projects.

        public static (bool valid, string message) IsValidCSVFile(this IFormFile file)
        {
            if (file == null || file.Length < 1)
                return (false, "File missing or empty in content");

            if (string.IsNullOrWhiteSpace(file.ContentType) || file.ContentType.Trim().ToLower() != "text/csv")
                return (false, "File is wrong content type");

            return (true, null);
        }

        public static async Task<byte[]> GetBytesAsync(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}