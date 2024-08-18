using System.Net;
using System.Net.Mail;
namespace Backend.SendEmail
{
    public class Email
    {
        private SmtpClient smtpClient;
        private MailMessage mailMessage;
        public Email(string name, string message){
             // Configuración del cliente SMTP
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(Environment.GetEnvironmentVariable("FROM_EMAIL"), Environment.GetEnvironmentVariable("FROM_PASS")),
                EnableSsl = true,
                UseDefaultCredentials=false,
                DeliveryMethod=SmtpDeliveryMethod.Network,
                
            };
            // Configuración del mensaje
            mailMessage = new MailMessage
            {
                From = new MailAddress(Environment.GetEnvironmentVariable("FROM_EMAIL")??""),
                Subject = "Contacto para contratación",
                Body = name+":\n"+message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(Environment.GetEnvironmentVariable("TO_EMAIL") ?? "");
        }
        public void SendEmail(){
            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Correo enviado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }
    }
}