using GlicareApp.Domain.Entities;
using MediatR;

namespace GlicareApp.Services.Commands;

public class CreateEscortCommand : IRequest<Escort>
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Relationship { get; set; }
    public string PacientId { get; set; }
}