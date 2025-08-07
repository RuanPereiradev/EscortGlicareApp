// -----------------------------------------------------------------------------
// 📄 Classe: GetAllEscortsQuery
// 📦 Namespace: GlicareApp.Services.Queries
//
// 🧠 O que é:
// É uma *query* (consulta) que será enviada ao MediatR para solicitar
// todos os acompanhantes (Escort) cadastrados no sistema.
//
// 🔧 Pra que serve:
// - Permitir que a Controller solicite os dados sem saber como buscar.
// - Facilitar a separação de responsabilidades e testes.
// -----------------------------------------------------------------------------

using GlicareApp.Domain.Entities;
using MediatR;

namespace GlicareApp.Services.Queries;

public class GetAllEscortsQuery : IRequest<List<Escort>>
{
    
}