// -------------------------------------------------------------------------------------------------------
// SolicitarTokenHandler.cs
// -------------------------------------------------------------------------------------------------------
// Responsável por processar o comando SolicitarTokenCommand.
// Esse comando é usado para iniciar o processo de verificação de e-mail de um novo usuário.
//
// Fluxo da lógica:
// 1. Verifica se já existe um usuário com o e-mail informado.
//    - Se existir, lança exceção informando que o e-mail já está em uso.
// 2. Verifica se já existe um token de verificação para esse e-mail.
//    - Se existir, atualiza o token existente.
//    - Caso contrário, cria um novo registro de token.
// 3. Envia o token para o e-mail do usuário através do serviço de envio de e-mails.
// 4. Retorna o ID do token gerado.
//
// Esse handler é parte da camada de comandos (Write side) e segue o padrão CQRS com MediatR.
// -------------------------------------------------------------------------------------------------------

using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Services.Commands;
using MediatR;

namespace GlicareApp.Services.CommandsHandlers;

public class RequestTokenHandler:IRequestHandler<RequestTokenCommand, string>
{
    private readonly IUserRepository _userRepository;
    
    private readonly ITokenRepository _tokenRepository;
    
    private readonly IEmailService _emailService;

    public RequestTokenHandler(
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _emailService = emailService;
    }

    public async Task<string> Handle(RequestTokenCommand request, CancellationToken cancellationToken)
    {
        var userExist = await _userRepository.ForEmailAsync(request.Email);

        if (userExist != null)
        {
            throw new Exception("Usuário já cadastrado com este e-mail.");
        }
        var existingToken = await _tokenRepository.ForEmailAsync(request.Email);

        var generatadToken = TokenGenerate();

        if (existingToken != null)
        {
            existingToken.TokenUpdate(generatadToken);
            await _tokenRepository.UpdateAsync(existingToken);
            
        }
        else
        {
            var newToken = new VerificationToken(request.Email, generatadToken);
            await _tokenRepository.AddAsync(newToken);
            existingToken = newToken;
        }

        await _emailService.SendTokenAsync(request.Email, generatadToken);
        return existingToken.Id;
    }

    private string TokenGenerate()
    {
        return Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
    }
}