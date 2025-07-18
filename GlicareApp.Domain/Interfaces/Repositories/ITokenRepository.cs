using GlicareApp.Domain.Entities;

namespace GlicareApp.Domain.Interfaces;

public interface ITokenRepository
{
    
    Task GetByEmailAsync(User user);
    Task<VerificationToken> ForEmailAsync(string email);
    
    Task AddAsync(VerificationToken token);
    Task<VerificationToken> GetByIdAsync(string id);

    Task UpdateAsync(VerificationToken token);
    Task RemoveAsync(string requestTokenId);
}