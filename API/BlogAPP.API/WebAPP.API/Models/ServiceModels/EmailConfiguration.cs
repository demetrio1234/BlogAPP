namespace WebAPP.API.Models.ServiceModels
{
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public EmailConfiguration() { }

        public EmailConfiguration(string from, string smtpServer, int port, string userName, string password)
        {
            From = from;
            SmtpServer = smtpServer;
            Port = port;
            UserName = userName;
            Password = password;
        }
    }
}
