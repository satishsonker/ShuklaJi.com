using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ShuklaJi.ModelLayer.Common
{
   public class CredentialModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isExternal { get; set; }
    }

    public class LoginTokenDataModel
    {
        public string Token { get; set; }
        public DataTable UserData { get; set; }
        public string Message { get; set; }
    }

    public class ChangePasswordModel
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class LoginTokenModel
    {
        public string SecureId { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Message { get; set; }
          public DataTable UserData { get; set; }
    }
}
