namespace CycleShare.Server.Dtos
{
    public class EmailDto
    {
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Endpoint { get; internal set; }
        public string Subject { get; internal set; }
    }
}
