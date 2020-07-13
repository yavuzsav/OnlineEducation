using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.Helpers;

namespace OnlineEducation.API.Helper
{
    public class VideoFile
    {
        [MaxFileSize(250000000)]
        [AllowExtensions(new []{".mp3", ".mp4", ""})]
        public IFormFile File { get; set; }
    }
}
