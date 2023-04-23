using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;

namespace OpenSaludSecurity.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        /// <summary>
        /// Utilizando el API key de SendGrid de la variable de ambiente (variable de app para Azure App Service), hace el llamado al API para enviar un correo con los detalles
        /// parametrizados.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        /// <summary>
        /// Ejecuta de manera asincrona el llamado al API de send grid para enviar el correo con los datos especificados.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="toEmail"></param>
        /// <returns></returns>
        public Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            Configuration.Default.ApiKey.TryAdd("api-key", apiKey);
            var apiInstance = new TransactionalEmailsApi();
            var from = new SendSmtpEmailSender("Open Salud", "nathy27ho@hotmail.com");
            var to = new List<SendSmtpEmailTo>() { new SendSmtpEmailTo(toEmail) };
            var sendSmtpEmail = new SendSmtpEmail(from, to, null, null, message, message, subject); // SendSmtpEmail | Values to send a transactional email
            try
            {
                // Send a transactional email
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                _logger.LogInformation($"Email to {toEmail} queued successfully!");
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Failure Email to {toEmail}");
                Debug.Print("Exception when calling AccountApi.GetAccount: " + e.Message);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
