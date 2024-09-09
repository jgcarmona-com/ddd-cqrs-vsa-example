using System.Text.Json.Serialization;

namespace Jgcarmona.Qna.Application.Auth.Models;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "bearer";
}