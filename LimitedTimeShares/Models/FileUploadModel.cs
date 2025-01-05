using System.ComponentModel.DataAnnotations;

namespace SecureFileUpload.Models
{
    public class FileUploadModel
    {
        [Required(ErrorMessage = "Please select a file")]
        public IFormFile File { get; set; } = default!; // Use `default!` to suppress warnings about non-nullable fields

        [Required]
        [Range(1, 168, ErrorMessage = "Link expiry time must be between 1 and 168 hours")]
        public int ExpiryHours { get; set; } = 24; // Default 24 hours

        public string? UploadMessage { get; set; }
        public string? ShareableLink { get; set; }
    }
}
