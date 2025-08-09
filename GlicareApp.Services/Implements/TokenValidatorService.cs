using GlicareApp.Domain.Interfaces.Repositories;

namespace GlicareApp.Services.Implements;

public class TokenValidatorService : ITokenValidatorService
{
    public bool ValidateToken(string token, string LoginType)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;
        switch (LoginType.ToUpper())
        {
            case "GOOGLE":
                return token == "TOKEN_GOOGLE";
            case "ICLOUD":
                return token == "TOKEN_ICLOUD";
            default:
                return false;
        }
    }
}

