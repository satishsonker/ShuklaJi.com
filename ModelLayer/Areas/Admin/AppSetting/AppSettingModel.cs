using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.ModelLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.AppSetting
{
   public class AppSettingModel
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
        [FromForm(Name = "appname")]
        public string AppName { get; set; }
        [FromForm(Name = "userid")]
        public int UserId { get; set; }
        [FromForm(Name = "defaultpassword")]
        public string DefaultPassword { get; set; }

        [FromForm(Name = "image")]
        public string Image { get; set; }

        [FromForm(Name = "resetlinkvalidity")]
        public int ResetLinkValidity { get; set; }
    }

    public class DowntimeModel: CommonPropertyModel
    {
        public int Id { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string Message { get; set; }
    }
}
