using System.IO;
using System.Net.Mail;
using Serenity.Data;
using Serenity;
using System;
using MimeKit;
using BMS_Scheduler.Common.Services;
using System.Collections.Generic;
using System.Data;
using Serenity.Services;

namespace BMS_Scheduler.Common
{
    public class EmailHelper
    {
        public static void Send(string subject, string body, string mailTo)
        {

            var message = new MimeMessage();
            if (mailTo == null)
                throw new ArgumentNullException(nameof(mailTo));
            message.To.Add(MailboxAddress.Parse(mailTo));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();
            Send(message);
        }

        public static void Send(MimeMessage message)
        {
            //using var connection = AppStatics.SqlConnections.NewFor<NotificationRow>();
            //var config = new MailingService().GetEmailConfig();
            //new MailingService().Enqueue(connection, message);
        }

        public void GenerateData(ApprovalProcess? processId, long? applicationId, int? fromUserId, string subject,
            string emailBody, string notificationBody, string url, bool isEmail, bool isNotification,
            bool isSms, string attachment, string fileName, string entityDialog,
            MailPriority priority,
            int? toUserId, string toAddress, string mobileNumber, string emailCC, string emailBcc
            )
        {
            //var notification = new NotificationRow
            //{
            //    ProcessId = processId,
            //    ApplicationId = applicationId,
            //    FromUserId = null,
            //    Subject = subject,
            //    EmailBody = emailBody,
            //    NotificationText = notificationBody,
            //    IsEmail = isEmail,
            //    IsNotification = isNotification,
            //    IsSms = isSms,
            //    AttachedFileName = fileName,
            //    Attachment = attachment,
            //    EntityDialog = entityDialog,
            //    //NotificationDetails = new List<NotificationDetailRow>
            //    //{
            //    //    new NotificationDetailRow
            //    //    {

            //    //    }
            //    //}
            //};
        }

        internal void SendEmailAndNotification(ISaveRequestHandler handler, string emailTemplate, string notificationTemplate)
        {
            var fromUserId = Convert.ToInt32(handler.Context.User.GetIdentifier());

            
        }
    }
}