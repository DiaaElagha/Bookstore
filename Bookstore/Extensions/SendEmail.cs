using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Bookstore.Extensions
{
    public class SendEmail
    {
        public static void SendMailResetPassword(String Email, String Name, String callbackUrl)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("Sender Email", "Password");
            objeto_mail.From = new MailAddress("Sender Email", "Title Email");
            objeto_mail.To.Add(new MailAddress(Email));
            objeto_mail.Subject = "Subject Email";
            string body = $"";
            body += "<b>Hello World</b>";

            ContentType mimeType = new ContentType("text/html");

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            objeto_mail.AlternateViews.Add(alternate);
            client.Send(objeto_mail);

        }
        public static void SendMail(String Email, String Body)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("Sender Email", "Password");
            objeto_mail.From = new MailAddress("Sender Email", "Title Email");
            objeto_mail.To.Add(new MailAddress(Email));
            objeto_mail.Subject = "Subject Email";
            string body = $"<!DOCTYPE HTML PUBLIC>";
            body += "<html><head></head>";
            body += "<body>";
            body += Body;
            body += "</body></html>";

            ContentType mimeType = new ContentType("text/html");

            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            objeto_mail.AlternateViews.Add(alternate);
            client.Send(objeto_mail);
        }
    }
}
