using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Product_Catalog.BLL.Validation
{


    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long _maxFileSize = 1 * 1024 * 1024; // 1 MB in bytes

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success; // Not a file, no validation required
            }

            // Validate file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
            {
                return new ValidationResult($"File type not allowed. Only {string.Join(", ", _allowedExtensions)} are accepted.");
            }

            // Validate file size
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"File size cannot exceed {_maxFileSize / (1024 * 1024)} MB.");
            }

            return ValidationResult.Success; // File is valid
        }
    }

}
