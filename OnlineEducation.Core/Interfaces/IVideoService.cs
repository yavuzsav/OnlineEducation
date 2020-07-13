using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.Helpers;

namespace OnlineEducation.Core.Interfaces
{
    public interface IVideoService
    {
        Task<UploadResult> UploadVideos(List<IFormFile> files);
        Task<UploadResult> UploadVideo(IFormFile file);
        Task<bool> DeleteVideos(List<string> publicIds);
        Task<bool> DeleteVideo(string publicId);
    }
}
