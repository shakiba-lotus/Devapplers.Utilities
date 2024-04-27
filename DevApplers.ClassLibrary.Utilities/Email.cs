using System;
using System.Net;
using System.Net.Mail;

namespace DevApplers.ClassLibrary.Utilities
{
    public static class Email
    {
        public static bool SendEmail(this string message, string subject,MailAddress sender, MailAddress receiver, string senderPassword,string smtpServer, int smtpPort)
        {
            try
            {
                    var senderEmail = sender;
                    var receiverEmail = receiver;
                    var password = senderPassword;
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = smtpServer,
                        Port = smtpPort,
                        EnableSsl = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }

                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}