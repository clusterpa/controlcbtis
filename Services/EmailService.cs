using System.Net;
using System.Net.Mail;

namespace controlcbtis.Services
{
    public class EmailService
    {
        private const string CorreoEmisor = "clusterpa727@gmail.com";
        private const string Password = "contra";
        private const string CorreoDestino = "lopezvillarrealluismario@gmail.com";

        public async Task EnviarPaseAsync(byte[] pdf)
        {
            var mensaje = new MailMessage();

            mensaje.From = new MailAddress(CorreoEmisor);
            mensaje.To.Add(CorreoDestino);

            mensaje.Subject = "PASE DE SALIDA";
            mensaje.Body = "Se adjunta el pase de salida.";

            mensaje.Attachments.Add(new Attachment(
                new MemoryStream(pdf),
                "PaseSalida.pdf",
                "application/pdf"));

            using var smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential(
                CorreoEmisor,
                Password);

            await smtp.SendMailAsync(mensaje);
        }
    }
}
