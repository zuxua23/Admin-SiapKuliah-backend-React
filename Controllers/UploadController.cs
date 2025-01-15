﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin_SiapKuliah_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UploadController(IWebHostEnvironment hostingEnvironment) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                string fileName = "FILE_" +
                    Guid.NewGuid() +
                    "_" +
                    DateTime.Now.Year +
                    DateTime.Now.Month +
                    DateTime.Now.Day +
                    DateTime.Now.Hour +
                    DateTime.Now.Minute +
                    DateTime.Now.Second;

                if (file == null || file.Length == 0)
                    return BadRequest("Berkas tidak ada/tidak valid.");

                string uploadDirectory = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadDirectory))
                    Directory.CreateDirectory(uploadDirectory);
                string filePath = Path.Combine(uploadDirectory, fileName + Path.GetExtension(file.FileName));

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                return Ok(JsonConvert.SerializeObject(new { Hasil = fileName + Path.GetExtension(file.FileName) }));
            }
            catch { return BadRequest(); }
        }

        [HttpGet("{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            // Tentukan path lengkap untuk file yang diminta
            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "Uploads", fileName);

            // Cek apakah file ada
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File tidak ditemukan.");
            }

            // Menentukan MIME type sesuai dengan ekstensi file
            var fileExtension = Path.GetExtension(fileName).ToLower();
            string mimeType = GetMimeType(fileExtension);

            // Membuka file sebagai FileStream
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Mengembalikan file sebagai respons dengan dukungan Range Requests
            Response.Headers.Add("Accept-Ranges", "bytes"); // Menginformasikan dukungan Range
            return File(stream, mimeType, enableRangeProcessing: true);
        }

        private string GetMimeType(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".mp4": return "video/mp4";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".pdf": return "application/pdf";
                default: return "application/octet-stream"; // Default MIME type
            }
        }
    }
}
