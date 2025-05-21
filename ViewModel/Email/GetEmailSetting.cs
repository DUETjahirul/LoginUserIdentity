namespace LoginUserIdentity.ViewModel.Email
{
    public class GetEmailSetting
    {
        public string? SecretKey { get; set; }
        public string? From { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
