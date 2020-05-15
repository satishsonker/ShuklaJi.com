using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.ModelLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.UserManagement
{
    public class AstrologerRatingModel:CommonPropertyModel
    {
        public int RatingId { get; set; }
        public int AstrologerId { get; set; }
        public string Stars { get; set; }
        public string Review { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }

    public class AstrologerScheduleModel
    {
        public int ScheduleId { get; set; }
        public int AstrologerId { get; set; }
        public int UserId { get; set; }
        public string Day { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string Remarks { get; set; }
        public bool Is24hours { get; set; }
    }
    public class AstrologerModel
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }

        [FromForm(Name = "AstrologerId")]
        public int AstrologerId { get; set; }

        [FromForm(Name = "Rating")]
        public int Rating { get; set; }

        [FromForm(Name = "Name")]
        public string Name { get; set; }

        [FromForm(Name = "Photo")]
        public string Photo { get; set; }

        [FromForm(Name = "Language")]
        public string Language { get; set; }

        [FromForm(Name = "Experties")]
        public string Experties { get; set; }

        [FromForm(Name = "Experience")]
        public decimal Experience { get; set; }

        [FromForm(Name = "Location")]
        public string Location { get; set; }

        [FromForm(Name = "Address")]
        public string Address { get; set; }

        [FromForm(Name = "Mobile")]
        public string Mobile { get; set; }

        [FromForm(Name = "Email")]
        public string Email { get; set; }

        [FromForm(Name = "Dob")]
        public DateTime Dob { get; set; }

        [FromForm(Name = "ConsultPrice")]
        public string ConsultPrice { get; set; }

        [FromForm(Name = "Biography")]
        public string Biography { get; set; }

        [FromForm(Name = "ConsultOn")]
        public string ConsultOn { get; set; }

        [FromForm(Name = "UserId")]
        public int UserId { get; set; }
    }
}
