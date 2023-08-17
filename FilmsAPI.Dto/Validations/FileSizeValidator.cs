using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FilmsAPI.Dto.Validations
{
    public class FileSizeValidator : ValidationAttribute
    {
        private readonly long _sizeMaxMB;

        public FileSizeValidator(long sizeMaxMB)
        {
            _sizeMaxMB = sizeMaxMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            IFormFile formFile = value as IFormFile;
            if (formFile == null) return ValidationResult.Success;
            if (formFile.Length > (_sizeMaxMB * 1024 * 1024))
                return new ValidationResult($"The maximum file size cannot exceed {_sizeMaxMB} MB");
            return ValidationResult.Success;
        }
    }
}