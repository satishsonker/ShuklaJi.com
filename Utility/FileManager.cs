using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ModelLayer.Common.ApiResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuklaJi.Utilities
{
    public class FileManager
    {
        public enum ImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }

        public enum FileType
        {
            Image,
            File
        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };              // PNG
            var tiff = new byte[] { 73, 73, 42 };                  // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                 // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };          // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };         // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;

            return ImageFormat.unknown;
        }       

        private static bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return GetImageFormat(fileBytes) != ImageFormat.unknown;
        }

        public static List<FileModel> GetFileData(ICollection<IFormFile> files,FileType fileType)
        {
            List<FileModel> fileModels = new List<FileModel>();
            if (files!=null)
            {
                try
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            FileModel fileModel = new FileModel();
                            if (fileType == FileType.Image && !CheckIfImageFile(file))
                            {
                                fileModel.IsValidFile = false;
                            }
                            else
                            {
                                using (var ms = new MemoryStream())
                                {
                                    file.CopyTo(ms);
                                    fileModel.DataByte = ms.ToArray();
                                    fileModel.DataBase64 = Convert.ToBase64String(fileModel.DataByte);
                                    fileModel.FileName = file.FileName;
                                    fileModel.FileExt = Path.GetExtension(file.FileName);
                                    fileModels.Add(fileModel);
                                    fileModel.IsValidFile = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return new List<FileModel>();
                }
            }
            return fileModels;
        }
        public FileResponseModel SaveFile(IFormFile files,string rootPath)
        {
            FileResponseModel fileResponseModel =null;
            if (files!=null && !string.IsNullOrEmpty(rootPath))
            {                
                var uploads = Path.Combine(rootPath, "wwwroot\\webFiles");
                
                if (files.Length > 0)
                {
                    Guid guid = Guid.NewGuid();
                    string serverFileName = $"{guid.ToString()}{Path.GetExtension(files.FileName)}";
                    var filePath = Path.Combine(uploads, $"webFiles_{serverFileName}");
                    if(!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        string apiUrl = AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.ApiUrl, string.Empty);
                        files.CopyToAsync(fileStream);
                        fileResponseModel=new FileResponseModel()
                        {
                            Filename = files.FileName,
                            Filepath = uploads,
                            ServerFilename = $"webFiles_{serverFileName}",
                            ServerUrl= apiUrl,
                            FileFullpath = $"{apiUrl}/webFiles_{serverFileName}",
                        };
                    }
                }
            }
                return fileResponseModel;
        }

        public class FileModel
        {
            public string FileName { get; set; }
            public string FileExt { get; set; }
            public string DataBase64 { get; set; }
            public byte[] DataByte { get; set; }
            public bool IsValidFile { get; set; }
        }
    }
}
