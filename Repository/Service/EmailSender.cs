using System.Net.Mail;
using LoginUserIdentity.Repository.Interface;
using LoginUserIdentity.ViewModel.Email;

namespace LoginUserIdentity.Repository.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration; // Configuration object to access app settings
        public EmailSender(IConfiguration configuration) // Constructor accepting IConfiguration
        {
            this.configuration = configuration; // Initializing the configuration object
        }
        public async Task<bool> EmailSendAsync(string email, string subject, string message)
        {
            bool status = false;
            try
            {
                GetEmailSetting getEmailSetting = new GetEmailSetting()
                {
                    SecretKey = configuration.GetValue<string>("AppSettings:SecretKey"), // Specifying the type argument explicitly
                    From = configuration.GetValue<string>("AppSettings:EmailSettings:From"), // Specifying the type argument explicitly
                    SmtpServer = configuration.GetValue<string>("AppSettings:EmailSettings:SmtpServer"), // Specifying the type argument explicitly
                    Port = configuration.GetValue<int>("AppSettings:EmailSettings:Port"), // Specifying the type argument explicitly
                    EnableSsl = configuration.GetValue<bool>("AppSettings:EmailSettings:EnablSsl") // Specifying the type argument explicitly
                }; // Create an instance of GetEmailSetting
                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(getEmailSetting.From), // Set the sender's email address
                    Subject = subject, // Set the email subject
                    Body = message, // Set the email body
                }; // Create a new MailMessage object
                mailMessage.To.Add(new MailAddress(email)); // Add the recipient's email address
                SmtpClient smtpClient = new SmtpClient(getEmailSetting.SmtpServer) // Create a new SmtpClient object
                {
                    Port = getEmailSetting.Port, // Set the SMTP server port
                    Credentials = new System.Net.NetworkCredential(getEmailSetting.From, getEmailSetting.SecretKey), // Set the credentials
                    EnableSsl = getEmailSetting.EnableSsl // Enable SSL if specified
                };
                await smtpClient.SendMailAsync(mailMessage); // Send the email asynchronously
                status = true; // Set status to true if email is sent successfully
            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
    }
}
