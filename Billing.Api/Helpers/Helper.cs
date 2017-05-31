using Billing.Api.Helpers.PDF;
using Billing.Database;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Web;

namespace Billing.Api.Helpers
{
    public static class Helper
    {
        public static int StatusCount { get { return Enum.GetValues(typeof(Status)).Length; } }

        public static int RegionCount { get { return Enum.GetValues(typeof(Region)).Length; } }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(string Message, string Level = "ERROR")
        {
            if (HttpContext.Current != null)
            {
                Message += ": " + HttpContext.Current.Request.Url.AbsoluteUri;
            }

            if (Level == "INFO") log.Info(Message); else log.Error(Message);
        }

        public static string Signature(string Secret, string AppId)
        {
            byte[] secret = Convert.FromBase64String(Secret);
            byte[] appId = Convert.FromBase64String(AppId);

            var provider = new System.Security.Cryptography.HMACSHA256(secret);
            string key = System.Text.Encoding.Default.GetString(appId);
            var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(key));

            return Convert.ToBase64String(hash);
        }

        public static void SendEmail(Invoice invoice, string from, string emailTo)
        {
            string subject = "Invoice - " + invoice.InvoiceNo;
            string FromMail = from;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("alpha2017team@gmail.com");
            mail.To.Add(emailTo);
            mail.Subject = subject;
            string body = "Hi," + Environment.NewLine + "Please find the attached file. For more information contact us at: " + FromMail + Environment.NewLine + Environment.NewLine + "Kind Regards," + Environment.NewLine + invoice.Agent.Name;
            mail.Body = body;

            InvoicePdf pdf = new InvoicePdf(invoice);

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false);

            pdfRenderer.Document = pdf.CreateDocument();

            pdfRenderer.RenderDocument();


            MemoryStream stream = new MemoryStream();

            pdfRenderer.Save(stream, false);



            mail.Attachments.Add(new Attachment(stream, "Invoice-" + DateTime.UtcNow.ToShortDateString() + ".pdf", MediaTypeNames.Application.Pdf));

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Port = 587;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential("alpha2017team@gmail.com", "alphateam");
            SmtpServer.Send(mail);
        }
    }
}