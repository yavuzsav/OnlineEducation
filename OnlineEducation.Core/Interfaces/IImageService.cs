using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.Helpers;

namespace OnlineEducation.Core.Interfaces
{
    public interface IImageService
    {
        Task<UploadResult> UploadImagesAsync(List<IFormFile> files, string folder = "");
        Task<UploadResult> UploadImageAsync(IFormFile file, string folder = "");
        Task<bool> DeleteImagesAsync(List<string> publicIds);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
