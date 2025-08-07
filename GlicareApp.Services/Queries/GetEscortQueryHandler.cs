// -----------------------------------------------------------------------------
// 📄 Classe: GetEscortQueryHandler
// 📦 Namespace: GlicareApp.Services.Queries
//
// 🧠 O que é:
// Esta classe é um "Query Handler" do padrão **CQRS** (Command Query Responsibility Segregation).
// Sua responsabilidade é lidar com a consulta de um acompanhante (Escort) a partir do seu ID,
// utilizando o MediatR para separar lógica de leitura da lógica de escrita.
//
// 🛠️ O que faz:
// - Recebe uma requisição `GetEscortByIdQuery` contendo o ID do acompanhante.
// - Consulta o repositório (`IEscortRepository`) para buscar o acompanhante correspondente.
// - Retorna o objeto `Escort` encontrado no banco.
//
// 🔁 Como funciona internamente:
// 1. O método `Handle` é chamado automaticamente pelo MediatR quando a query é disparada.
// 2. Ele utiliza o repositório `IEscortRepository` para buscar o acompanhante pelo ID fornecido.
// 3. Retorna a entidade `Escort` encontrada (ou null, caso não exista — depende da implementação do repositório).
//
// 🔗 Integrações e dependências:
// - Utiliza o padrão Mediator (MediatR) para desacoplar a lógica de busca.
// - Depende da interface `IEscortRepository` para o acesso a dados da entidade `Escort`.
// - A operação é assíncrona e suporta `CancellationToken`.
//
// 📌 Observações:
// - Essa classe representa a parte de **leitura** (queries) do sistema seguindo os princípios do CQRS.
// - Pode ser estendida futuramente para incluir validações, cache, logs, etc., se necessário.
//
// -----------------------------------------------------------------------------
using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces.Repositories;
using MediatR;

namespace GlicareApp.Services.Queries;

public class GetEscortQueryHandler : IRequestHandler<GetEscortByIdQuery, Escort>
{
    private readonly IEscortRepository _escortRepository;

    public GetEscortQueryHandler(IEscortRepository escortRepository)
    {
        _escortRepository = escortRepository;
    }
    public async Task<Escort> Handle(GetEscortByIdQuery request, CancellationToken cancellationToken)
    {
        return await _escortRepository.GetIdAsync(request.Id);

    }
}

