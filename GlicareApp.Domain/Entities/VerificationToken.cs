namespace GlicareApp.Domain.Entities;

public class VerificationToken
{
    public string Id { get; private set; } =  Guid.NewGuid().ToString();
    public string Email { get; private set; }
    public string Token { get; private set; }
    public string GeneratedAt { get; private set; }

    public VerificationToken(string  email, string token)
    {
        Email = email;
        Token = token;
        GeneratedAt = DateTime.UtcNow.ToString("o"); // ISO 8601 string

    }

    public void TokenUpdate(string newToken)
    {
        Token = newToken;
        GeneratedAt = DateTime.UtcNow.ToString("o");
    }
    
    

}