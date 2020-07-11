using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VegaStore.UI.ValidationAttributes
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedFileExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var ext = Path.GetExtension(file.FileName).Trim().ToLower();
                if (!_extensions.Contains(ext))
                    return new ValidationResult("Invalid file extension.");
            }


            return ValidationResult.Success;
        }
    }
}
