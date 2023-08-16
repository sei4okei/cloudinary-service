using CloudinaryDotNet.Actions;

namespace CloudianryService.Interfaces
{
    public interface IPhotoService
    {
        public Task<Models.UploadResult> AddPhotoAsync(IFormFile file);
        public Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
