using CloudianryService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudianryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService _photoService)
        {
            photoService = _photoService;
        }

        [HttpPut]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            var isAdded = await photoService.AddPhotoAsync(file);

            if (isAdded.Error != null) return BadRequest(isAdded);

            return Ok(isAdded);
        }

        [HttpDelete("{publicId}")]
        public async Task<IActionResult> DeletePhoto(string publicId)
        {
            var isDeleted = await photoService.DeletePhotoAsync(publicId);

            if (isDeleted.Error != null) return BadRequest(isDeleted);

            return Ok(isDeleted);
        }
    }
}
