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
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = email.Asunto,
                Body = email.Contenido,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}
