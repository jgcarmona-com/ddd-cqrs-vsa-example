namespace Jgcarmona.Qna.Common.Configuration
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = "Jgcarmona.Qna";
        public string Audience { get; set; } = "Jgcarmona.QnaUsers";
    }
}
