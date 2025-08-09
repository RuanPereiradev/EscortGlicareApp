// -----------------------------------------------------------------------------
// 📄 Classe: CreateEscortCommandHandler
// 📦 Namespace: GlicareApp.Services.CommandsHandlers
//
// 🧠 O que é:
// Esta classe é um "Command Handler" do padrão **CQRS** (Command Query Responsibility Segregation)
// e é responsável por processar o comando de criação de um novo acompanhante (`CreateEscortCommand`).
//
// 🛠️ O que faz:
// - Recebe os dados de um acompanhante (nome, telefone, e-mail, etc.) vindos do comando.
// - Valida se o paciente (relacionado ao acompanhante) existe no banco de dados.
// - Cria uma nova entidade `Escort` com os dados recebidos.
// - Salva o acompanhante no banco através do repositório (`IEscortRepository`).
// - Retorna o objeto `Escort` já com o ID gerado.
//
// 🔁 Como funciona internamente:
// 1. Verifica se o paciente informado existe (consultando o `IPacientRepository`).
// 2. Se não existir, lança uma exceção.
// 3. Se existir, instancia um novo objeto `Escort` com os dados do comando.
// 4. Usa o `IEscortRepository` para salvar esse acompanhante no banco.
// 5. Registra logs com Serilog, tanto para sucesso quanto para erro.
// 6. Retorna o acompanhante criado.
//
// 🔗 Integrações e dependências:
// - Usa o padrão Mediator (via biblioteca MediatR) para lidar com comandos.
// - Depende de `IEscortRepository` e `IPacientRepository` para acessar os dados no banco.
// - Usa `Serilog` para geração de logs estruturados.
// - A operação é assíncrona, com suporte a `CancellationToken`.
//
// 📌 Observações:
// - Se o paciente informado não existir, o sistema lança uma `ArgumentException`.
// - É uma classe típica de aplicação do padrão **Clean Architecture**, separando regras de negócio
//   e acesso a dados.
//
// -----------------------------------------------------------------------------

using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Services.Commands;
using MediatR;
using Serilog;

namespace GlicareApp.Services.CommandsHandlers
{
    public class CreateEscortCommandHandler : IRequestHandler<CreateEscortCommand, Escort>
    {
        private readonly IEscortRepository _escortRepository;
        private readonly IPacientRepository _pacientRepository;
        private readonly ILogger _logger;
        private readonly ITokenValidatorService _tokenValidatorService;

        public CreateEscortCommandHandler(IEscortRepository escortRepository, IPacientRepository pacientRepository, ITokenValidatorService tokenValidatorService)
        {
            _escortRepository = escortRepository;
            _pacientRepository = pacientRepository;
            _tokenValidatorService = tokenValidatorService;
            _logger = Log.ForContext<CreateEscortCommandHandler>();
        }

        public async Task<Escort> Handle(CreateEscortCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exist = await _pacientRepository.ExistsAsync(request.PacientId);
                if (!exist)
                { 
                    throw new ArgumentException("Paciente does not exist()");
                }

                var existingEscort = await _escortRepository.GetPacientByIdAsync(request.PacientId);
                if (existingEscort != null)
                {
                    throw new ArgumentException("Paciente already has an escort");
                }

                if (!_tokenValidatorService.ValidateToken(request.TokenAuth, request.LoginType))
                {
                    throw new ArgumentException("Invalid token");
                }
                
                

                var escort = new Escort()
                {
                    Name = request.Name,
                    Phone = request.Phone,
                    Email = request.Email,
                    Relationship = request.Relationship,
                    PacientId = request.PacientId
                    
                };

                var createdId = await _escortRepository.InsertEscortAsync(escort);
                escort.Id = createdId;
                _logger.Information("Successfully created acompanhante with ID {EscortId}", createdId);
                
                return escort;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating acompanhante for paciente {PacienteId}", request.PacientId);
                throw;
            }
        }
    }
}