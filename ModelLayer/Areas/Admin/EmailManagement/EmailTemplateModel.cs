using ShuklaJi.ModelLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.EmailManagement
{
   public class EmailSaveTemplateModel: CommonPropertyModel
    {
        public string EmailTemplate { get; set; }
        public string EmailTemplateName { get; set; }
        public string Subject { get; set; }
    }
}
