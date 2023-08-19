using System.ComponentModel.DataAnnotations;
using FilmsAPI.Dto.Constants;
using Microsoft.AspNetCore.Http;

namespace FilmsAPI.Dto.Validations
{
    public class FileTypeValidator : ValidationAttribute
    {
        private readonly string[] _validTypes;

        public FileTypeValidator(string[] validTypes)
        {
            _validTypes = validTypes;
        }

        public FileTypeValidator(FileTypes fileTypes)
        {
            if (fileTypes == FileTypes.Image)
            {
                _validTypes = new string[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            IFormFile formFile = value as IFormFile;
            if (formFile == null) return ValidationResult.Success;
            if (!_validTypes.Contains(formFile.ContentType))
                return new ValidationResult($"File type not allowed, valid types: {string.Join(", ", _validTypes)}");
            return ValidationResult.Success;
        }
    }
}