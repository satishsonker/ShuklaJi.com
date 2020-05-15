using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Common
{
   public class CommonPropertyModel
    {
        public int UserId { get; set; }
        public string AppName { get; set; }
    }

    public class CommonFileUploadModel
    {
        [FromForm(Name="file")]
        public IFormFile File { get; set; }

        [FromForm(Name = "Userid")]
        public int UserId { get; set; }

        [FromForm(Name = "Appname")]
        public string AppName { get; set; }
    }
}
