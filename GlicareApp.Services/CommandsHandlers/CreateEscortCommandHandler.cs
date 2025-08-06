using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Services.Commands;
using MediatR;

namespace GlicareApp.Services.CommandsHandlers;

public class CreateEscortCommandHandler : IRequestHandler<CreateEscortCommand, Escort>
{
    private readonly IEscortRepository _escortRepository;
    private readonly IPacientRepository _pacientRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateEscortCommandHandler(IEscortRepository escortRepository, IUnitOfWork unitOfWork, IPacientRepository pacientRepository)
    {
        _escortRepository = escortRepository;
        _unitOfWork = unitOfWork;
        _pacientRepository = pacientRepository;
    }
    
    public async Task<Escort> Handle(CreateEscortCommand request, CancellationToken cancellationToken)
    {
        var pacient = await _pacientRepository.GetPatientByIdAsync(request.PacientId);
        if (pacient == null)
        {
            throw new ArgumentException("Pacient not found.");
        }
        var escort = new Escort
        {
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            Relationship = request.Relationship,
        };

        _unitOfWork.BeginTransaction();
        try
        {
            await _escortRepository.InsertEscortAsync(escort);
            _unitOfWork.Commit();
            return escort;
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}