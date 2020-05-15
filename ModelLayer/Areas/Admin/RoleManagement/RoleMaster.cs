using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.RoleManagement
{
   public class AddRoleModel
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
        public string RoleDisplayName { get; set; }
        public int RoleId { get; set; } = 0;
    }
}
