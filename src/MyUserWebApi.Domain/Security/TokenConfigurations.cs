namespace MyUserWebApi.Domain.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Seconds { get; set; }
    }
}