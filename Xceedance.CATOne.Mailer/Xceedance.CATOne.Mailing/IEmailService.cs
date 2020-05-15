#region History
/*
Copyright
FileName: IMailService.cs
Created On: 20 June 2019
Revisied On: 
Revision History:
*/
#endregion

#region Namespaces
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xceedance.CATOne.DataModel;
using Xceedance.CATOne.DataModel.MailingModel;
#endregion

#region Class
namespace Xceedance.CATOne.Mailing
{
    /// <summary>
    /// Interface for the MailService class methods
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Definition for smtp mail sending method
        /// </summary>
        /// <param name="mailModel"></param>
        /// <returns></returns>
        void Send(EmailMessage emailMessage);

        /// <summary>
        /// Definition for pop3 incoming mail method
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        List<EmailMessage> ReceiveEmailViaPopServer(int maxCount = 10);

        /// <summary>
        /// Definition for Imap incoming mail method
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        List<EmailMessage> ReceiveEmailViaImapServer(int maxCount = 10);
    }
}
#endregion