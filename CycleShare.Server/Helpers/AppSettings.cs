namespace CycleShare.Server.Helpers
{
    public class AppSettings
    {
        public string Host { get; set; }
        public JWT Jwt { get; set; }
        public UserSettings UserSettings { get; set; }
        public EmailEntitySettings InvitationSettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
        public StorageSettings StorageSettings { get; set; }
        public EmailEntitySettings ForgotPasswordSettings { get; set; }
    }

    public class StorageSettings
    {
        public string AccountName { get; set; }
        public string AccessKey { get; set; }
        public string ContainerNamePrefix { get; set; }
        public string EndpointSuffix { get; set; }
    }

    public class EmailEntitySettings
    {
        public int ExpiryTimeInHours { get; set; }
        public string MailSubject { get; set; }
        public string Endpoint { get; set; }
    }

    public class UserSettings
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class JWT
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double ExpirationPeriod { get; set; }
    }

    public class EmailSettings
    {

        public int Port { get; set; }

        public string Domain { get; set; }

        public string UsernameEmail { get; set; }

        public string UsernamePassword { get; set; }

        public string FromEmail { get; set; }

        public string FromName { get; set; }
    }
}
