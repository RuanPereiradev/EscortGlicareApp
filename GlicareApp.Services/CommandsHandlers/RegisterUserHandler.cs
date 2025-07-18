using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces;
using GlicareApp.Services.Commands;
using MediatR;

namespace GlicareApp.Services.CommandsHandlers;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(ITokenRepository tokenRepository, IUserRepository userRepository)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var tokenEntity = await _tokenRepository.GetByIdAsync(request.TokenId);
        if (tokenEntity == null || tokenEntity.Token != request.Token)
            throw new ApplicationException("Invalid token");

        if (request.TermsUse == "false") 
            throw new ApplicationException("You must accept the terms of use.");
        
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
            throw new ApplicationException("Name and email are required.");
        
        // 4. Check if email already exists
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new ApplicationException("Email is already registered.");
        
        // 5. Hash password if provided
        string hashedPassword = null;
        if (!string.IsNullOrEmpty(request.Password))
        {
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }


        var user = new User(
            name: request.Name,
            email: request.Email,
            passwordHash: hashedPassword,
            birthDate: request.DateBirth,
            termsAccepted: request.TermsUse,
            registerType: request.RegisterType,
            loginType: request.LoginType

        );
        await _userRepository.AddAsync(user);
        
        await _tokenRepository.RemoveAsync(request.TokenId);

        return user.Id;
    }
}