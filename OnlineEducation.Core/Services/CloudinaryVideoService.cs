﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Helpers;
using OnlineEducation.Core.Interfaces;
using UploadResult = OnlineEducation.Core.Helpers.UploadResult;

namespace OnlineEducation.Core.Services
{
    public class CloudinaryVideoService : IVideoService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryVideoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<UploadResult> UploadVideosAsync(List<IFormFile> files, string folder = "")
        {
            var uploadResult = new VideoUploadResult();

            foreach (var file in files)
            {
                if (file.Length <= 0) continue;

                await using var stream = file.OpenReadStream();
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = folder
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                    throw new Exception(uploadResult.Error.Message);
            }

            return new UploadResult
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                CreatedAt = uploadResult.CreatedAt
            };
        }

        public async Task<UploadResult> UploadVideoAsync(IFormFile file, string folder = "")
        {
            if (file.Length <= 0) throw new Exception(ExceptionMessages.VideoUploadError);

            await using var stream = file.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folder
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return new UploadResult
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId,
                CreatedAt = uploadResult.CreatedAt
            };
        }

        public async Task<bool> DeleteVideosAsync(List<string> publicIds)
        {
            var result = new DeletionResult();

            foreach (var publicId in publicIds)
            {
                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Video
                };
                result = await _cloudinary.DestroyAsync(deleteParams);

                if (result.Error != null) throw new Exception(ExceptionMessages.VideoDeleteError);
            }

            return result.Result == "ok";
        }

        public async Task<bool> DeleteVideoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Video
            };
            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result.Result == "ok";
        }
    }
}
