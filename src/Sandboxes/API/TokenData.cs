namespace API
{
    public class TokenData
    {
        public const string ConfigurationEntry = "Token";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SigningSecret { get; set; }
    }
}
