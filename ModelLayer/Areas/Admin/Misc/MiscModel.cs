using System;
using System.Collections.Generic;
using System.Text;

namespace ShuklaJi.ModelLayer.Areas.Admin.Misc
{
   public class RefLookupModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
        public string Key { get; set; }

    }
}
