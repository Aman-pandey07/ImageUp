using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting; // Add this using directive
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUp.Data.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions);
        void DeleteFileAsync(string fileNameWithExtension);
    }

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public void DeleteFileAsync(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Invalid file path");
            }

            File.Delete(path);
        }

        public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowedFileExtensions)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            var contentPath = _environment.ContentRootPath;
            var uploadsFolderPath = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var ext = Path.GetExtension(imageFile.FileName);

            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed");
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var fileNameWithPath = Path.Combine(uploadsFolderPath, fileName);

            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            return fileName;
        }
    }
}
