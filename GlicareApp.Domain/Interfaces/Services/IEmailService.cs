namespace GlicareApp.Domain.Interfaces.Repositories;

public interface IEmailService
{
    Task SendTokenAsync(string email, string token);
}