namespace GlicareApp.Domain.Interfaces.Repositories;

public interface ITokenValidatorService
{
    bool ValidateToken(string token, string LoginType);
}