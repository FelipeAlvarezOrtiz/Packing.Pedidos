using System.Net;
using System.Net.Mail;
using Packing.Pedidos.Interfaces;

namespace Packing.Pedidos.Infraestructura
{
    public sealed class Mailer : IMailer
    {
        private readonly ILogger<Mailer> _logger;
        private readonly IConfiguration _configuration;

        public Mailer(ILogger<Mailer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task EnviarEmail(List<string> destinatarios, List<string> destinatariosOcultos, string asunto, string cuerpo)
        {
            var correo = _configuration.GetValue<string>("Mail:Correo");
            var alias = _configuration.GetValue<string>("Mail:Alias");
            var host = _configuration.GetValue<string>("Mail:Host") ?? throw new ArgumentNullException("Mail:Host");
            var puerto = _configuration.GetValue<int>("Mail:Puerto");
            var enableSsl = _configuration.GetValue<bool>("Mail:EnabledSsl");
            var fromAddress = new MailAddress(correo, alias);
            const string fromPassword = "phzjekavfsapfhyp";
            var subject = asunto;
            var body = cuerpo;
            var mensaje = new MailMessage();
            try
            {
                mensaje.From = fromAddress;
                mensaje.Subject = subject;
                mensaje.Body = body;
                mensaje.IsBodyHtml = true;

                foreach (var destinatario in destinatarios)
                {
                    mensaje.To.Add(new MailAddress(destinatario));
                }

                foreach (var destinatariosOculto in destinatariosOcultos)
                {
                    mensaje.Bcc.Add(new MailAddress(destinatariosOculto));
                }
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = puerto,
                    EnableSsl = enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                await smtp.SendMailAsync(mensaje);
                _logger.LogInformation("CORREO ENVIADO.");
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                throw;
            }
        }

    }
}
