using MediatR;

namespace GlicareApp.Services.Commands;

public class RequestTokenCommand : IRequest<string>
{
    public string Email { get; set; }   
}