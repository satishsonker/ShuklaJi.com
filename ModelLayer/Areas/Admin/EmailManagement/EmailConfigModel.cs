using ShuklaJi.ModelLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.EmailManagement
{
   public class EmailConfigModel: CommonPropertyModel
    {
        public int Port { get; set; }
        public string ServerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ConfigId { get; set; }
        public string ConfigName { get; set; }
    }
}
