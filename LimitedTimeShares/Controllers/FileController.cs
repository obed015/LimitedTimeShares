using Microsoft.AspNetCore.Mvc;
using SecureFileUpload.Models;
using SecureFileUpload.Services;

namespace SecureFileUpload.Controllers
{
    public class FileController : Controller
    {
        private readonly BlobStorageService _blobStorageService;

        public FileController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public IActionResult Upload()
        {
            return View(new FileUploadModel());
        }

        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                string shareableLink = await _blobStorageService.UploadFileAsync(model.File, model.ExpiryHours);
                return RedirectToAction("Success", new { link = shareableLink });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error uploading file: {ex.Message}");
                return View(model);
            }
        }

        public IActionResult Success(string link)
        {
            return View(model: link);
        }
    }
}
