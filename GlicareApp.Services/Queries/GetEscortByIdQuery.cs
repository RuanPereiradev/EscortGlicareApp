// -----------------------------------------------------------------------------
// 📄 Classe: GetEscortByIdQuery
// 📦 Namespace: GlicareApp.Services.Queries
//
// 🧠 O que é:
// Esta classe representa uma **query** no padrão **CQRS** (Command Query Responsibility Segregation).
// Ela é usada para requisitar os dados de um acompanhante (`Escort`) com base no seu ID.
//
// 🛠️ O que faz:
// - Armazena o identificador (`Id`) do acompanhante que se deseja consultar.
// - É enviada através do MediatR para ser processada por um QueryHandler.
// - Espera como resposta um objeto da entidade `Escort`.
//
// 🔁 Como funciona internamente:
// 1. É instanciada com o ID de um acompanhante.
// 2. É passada para o MediatR, que a direciona ao `GetEscortQueryHandler` correspondente.
// 3. O handler usa o ID para buscar os dados no banco via repositório.
//
// 🔗 Integrações e dependências:
// - Implementa `IRequest<Escort>` da biblioteca MediatR.
// - Está associada ao `GetEscortQueryHandler`, que trata essa query.
//
// 📌 Observações:
// - Esta classe é apenas um **DTO (Data Transfer Object)** simples com um único parâmetro.
// - Serve como parte da separação entre lógica de aplicação e domínio (Clean Architecture).
//
// -----------------------------------------------------------------------------

using GlicareApp.Domain.Entities;
using MediatR;

namespace GlicareApp.Services.Queries
{
    public class GetEscortByIdQuery : IRequest<Escort>
    {
        public string Id { get; set; }
    
        public GetEscortByIdQuery(string id)
        {
            Id = id;
        }
    }
}