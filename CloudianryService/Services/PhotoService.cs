using CloudianryService.Helpers;
using CloudianryService.Interfaces;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;

namespace CloudianryService.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );

            cloudinary = new Cloudinary(account);
        }

        public async Task<Models.UploadResult> AddPhotoAsync(IFormFile file)
        {
            Models.UploadResult result = new Models.UploadResult();

            if (!(file.Length > 0)) return new Models.UploadResult
            {
                Error = "File size too small"
            };

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null) return new Models.UploadResult
            {
                Error = "File type don't match"
            };

            result = new Models.UploadResult
            {
                Url = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId
            };

            return result;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await cloudinary.DestroyAsync(deleteParams);
        }
    }
}
