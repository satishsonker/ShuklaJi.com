#region History
/*
Copyright
FileName: IEmailCongiguration.cs
Created On: 20 June 2019
Revisied On: 
Revision History:
*/
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Text;
#endregion

#region Class
namespace Xceedance.CATOne.Mailing
{
    /// <summary>
    /// Interface defining properties for Email configuration
    /// </summary>
    public interface IEmailConfiguration
    {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }

        string PopServer { get; }
        int PopPort { get; }
        string PopUsername { get; }
        string PopPassword { get; }

        string ImapServer { get; }
        int ImapPort { get; }
        string ImapUsername { get; }
        string ImapPassword { get; }
    }

    /// <summary>
    /// Class implementing properties from the Interface
    /// </summary>
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }

        public string PopServer { get; set; }
        public int PopPort { get; set; }
        public string PopUsername { get; set; }
        public string PopPassword { get; set; }

        public string ImapServer { get; set; }
        public int ImapPort { get; set; }
        public string ImapUsername { get; set; }
        public string ImapPassword { get; set; }

        public string DefaultSender { get; set; }
    }
}
#endregion