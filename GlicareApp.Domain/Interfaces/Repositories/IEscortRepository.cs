// -----------------------------------------------------------------------------
// 📄 Interface: IEscortRepository
// 📦 namespace GlicareApp.Domain.Interfaces.Repositories;
// 
//
// 🧠 O que ela representa:
// Essa interface define o contrato para um repositório de "Escort", ou seja,
// os métodos que qualquer classe que implemente essa interface deve ter
// para lidar com dados relacionados a escort => acmpanhante.
//
// 📌 Por que usar:
// - Permite separar a lógica de acesso a dados da lógica de negócios.
// - Facilita a troca ou o mock de repositórios (por exemplo, para testes).
//
// 🔧 Métodos definidos:
// - GetAllEscortsAsync(): retorna todos os acompanhantes.
// - GetIdAsync(string id): retorna um acompanhante específico pelo ID.
// - InsertEscortAsync(escort escort): cria um novo acomanhante.
// - UpdateEscortAsync(int id, escort escort): atualiza um acompanhante existente.
// - DeleteAsync(string id): deleta um  pelo ID.
// -----------------------------------------------------------------------------

using GlicareApp.Domain.Entities;

namespace GlicareApp.Domain.Interfaces.Repositories;

public interface IEscortRepository
{
    Task<List<Escort>> GetAllEscortsAsync();
    Task<Escort> GetIdAsync(string id);
    Task<string> InsertEscortAsync(Escort escort);
    Task<string> UpdateEscortAsync(Escort escort);
    Task<string> DeleteEscortAsync(string id);
}