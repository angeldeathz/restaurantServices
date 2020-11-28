using System.Net;
using System.Net.Mail;

namespace RestaurantServices.Restaurant.Shared.Mail
{
    public class EmailClient
    {
        public void Send(Email email)
        {
            var fromAddress = new MailAddress("contactorestaurantesigloxxi@gmail.com", "Restaurante Siglo XXI");
            var toAddress = new MailAddress(email.ReceptorEmail, email.ReceptorNombre);
            const string fromPassword = "restaurante21";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = email.Asunto,
                Body = email.Contenido,
                IsBodyHtml = true
            };

            if (email.UrlAdjunto != null && email.UrlAdjunto.Count > 0)
            {
                foreach (var x in email.UrlAdjunto)
                {
                    mailMessage.Attachments.Add(new Attachment(x));
                }
            }
            
            using (var message = mailMessage)
            {
                smtp.Send(message);
            }
        }
    }
}
