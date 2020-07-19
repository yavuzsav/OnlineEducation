using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.Helpers;

namespace OnlineEducation.Core.Interfaces
{
    public interface IVideoService
    {
        Task<UploadResult> UploadVideosAsync(List<IFormFile> files, string folder = "");
        Task<UploadResult> UploadVideoAsync(IFormFile file, string folder = "");
        Task<bool> DeleteVideosAsync(List<string> publicIds);
        Task<bool> DeleteVideoAsync(string publicId);
    }
}
