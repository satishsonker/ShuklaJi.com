using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Common.ApiResponse
{
   public class FileResponseModel
    {
        public string Filename { get; set; }
        public string ServerFilename { get; set; }
        public string Filepath { get; set; }
        public string ServerUrl { get; set; }
        public string FileFullpath { get; set; }

    }

    public class FileUploadModel
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }

    }
}
