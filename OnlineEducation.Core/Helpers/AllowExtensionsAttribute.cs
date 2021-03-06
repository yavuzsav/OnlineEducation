﻿using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OnlineEducation.Core.Helpers
{
    public class AllowExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension != null && !_extensions.Contains(extension.ToLower()))
                    return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            var result = String.Join(", ", _extensions);
            return $"This file extension is not allowed! Allowed file extensions {result}";
        }
    }
}
