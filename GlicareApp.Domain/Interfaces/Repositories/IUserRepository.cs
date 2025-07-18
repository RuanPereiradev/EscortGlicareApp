using GlicareApp.Domain.Entities;

namespace GlicareApp.Domain.Interfaces;

public interface IUserRepository
{
    
    Task<User> GetByIdAsync(string id);
    Task<User> GetByEmailAsync(string email);
    
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserById(string id);
    Task<User> ForEmailAsync(string email);
    
    Task AddAsync(User user);
}