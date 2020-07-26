using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.Helpers;

namespace OnlineEducation.API.Helper
{
    public class ImageFile
    {
        [MaxFileSize(50000000)]
        [AllowExtensions(new []{".png", ".jpg", ".jpeg"})]
        public IFormFile File { get; set; }
    }
}
