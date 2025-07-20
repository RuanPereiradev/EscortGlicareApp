using GlicareApp.Domain.Entities;

namespace GlicareApp.Domain.Interfaces.Repositories;

public interface ITokenRepository
{
    
    Task<VerificationToken?> GetByEmailAsync(User user);
    Task<VerificationToken> ForEmailAsync(string email);
    
    Task AddAsync(VerificationToken token);
    Task<VerificationToken> GetByIdAsync(string id);

    Task UpdateAsync(VerificationToken token);
    Task RemoveAsync(string requestTokenId);
}