using MediatR;

namespace GlicareApp.Services.Commands;

public class RegisterUserCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DateBirth { get; set; }
    
    public string TermsUse { get; set; }
    public string RegisterType { get; set; }
    public string LoginType { get; set; }
    public string TokenId { get; set; }
    public string Token { get; set; }
    
}