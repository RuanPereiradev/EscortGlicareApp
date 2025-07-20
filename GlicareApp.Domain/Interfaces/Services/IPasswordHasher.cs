namespace GlicareApp.Domain.Interfaces.Repositories;

public interface IPasswordHasher
{
    string HashPassword(string password);
}