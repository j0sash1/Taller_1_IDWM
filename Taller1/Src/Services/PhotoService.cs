using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Taller1.Src.Interfaces;

namespace Taller1.Src.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IConfiguration config)
        {
            var acc = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}