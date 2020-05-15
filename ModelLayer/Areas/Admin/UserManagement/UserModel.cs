using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShuklaJi.ModelLayer.Common;
using System;

namespace ShuklaJi.ModelLayer.Areas.Admin.UserManagement
{
    public class UserModel:CommonPropertyModel
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }

        [FromForm(Name = "Userid")]
        public new int UserId { get; set; }

        [FromForm(Name = "UserName")]
        public string UserName { get; set; }

        [FromForm(Name = "Password")]
        public string Password { get; set; }

        [FromForm(Name = "RoleId")]
        public int RoleId { get; set; }

        [FromForm(Name = "FirstName")]
        public string FirstName { get; set; }

        [FromForm(Name = "LastName")]
        public string LastName { get; set; }

        [FromForm(Name = "Email")]
        public string Email { get; set; }

        [FromForm(Name = "Mobile")]
        public string Mobile { get; set; }

        [FromForm(Name = "Gender")]
        public string Gender { get; set; }

        [FromForm(Name = "CreatedBy")]
        public int CreatedBy { get; set; }

        [FromForm(Name = "Dob")]
        public DateTime Dob { get; set; }

        [FromForm(Name = "Photo")]
        public string Photo { get; set; }
    }

    public class ExternalUserModel:UserModel
    {
        public bool IsExternalUser { get; set; }
        public string Provider { get; set; }
        public string ProviderKey { get; set; }
    }
}
