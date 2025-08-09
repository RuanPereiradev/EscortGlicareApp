// -----------------------------------------------------------------------------
// 📄 Classe: CreateEscortCommand
// 📦 Namespace: GlicareApp.Services.Commands
//
// 🧠 O que é:
// Esta classe representa um **Command** (comando) do padrão **CQRS** (Command Query Responsibility Segregation).
// Ela é usada para solicitar a criação de um novo acompanhante (Escort) no sistema.
//
// 🛠️ O que faz:
// - Armazena os dados necessários para cadastrar um novo acompanhante:
//   nome, telefone, e-mail, grau de parentesco e o ID do paciente vinculado.
// - Quando enviada via MediatR, ela será tratada pelo `CreateEscortCommandHandler`,
//   que processa os dados e realiza o cadastro no banco de dados.
//
// 🔁 Como funciona internamente:
// 1. A classe é preenchida com os dados do acompanhante.
// 2. É enviada ao MediatR com `_mediator.Send(...)`.
// 3. O MediatR encaminha o comando para o handler correspondente (`CreateEscortCommandHandler`).
// 4. O handler valida, cria a entidade e persiste os dados no banco.
//
// 🔗 Integrações e dependências:
// - Implementa `IRequest<Escort>` da biblioteca MediatR, indicando que espera como resposta
//   um objeto da entidade `Escort`.
// - Está associada ao handler `CreateEscortCommandHandler`.
//
// 📌 Observações:
// - É parte da lógica de **escrita** do sistema (commands).
// - Segue os princípios da **Clean Architecture**, separando responsabilidades e facilitando testes.
//
// -----------------------------------------------------------------------------

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
    
    public string LoginType { get; set; }
    
    public string TokenAuth { get; set; }
    
    public bool UseTerms { get; set; }
    
}