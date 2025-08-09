// -----------------------------------------------------------------------------
// 📄 Classe: Escort
// 📦 namespace GlicareApp.Domain.Entities;
// 
//
// 🧠 O que ela representa:
// Essa classe é uma *entidade* que representa um Acompanhante no domínio da aplicação.
// É usada para mapear os dados de um acompanhante que serão salvos ou lidos do banco.
//
// 🧱 Propriedades:
// - Id: identificador único do acompanhante.
// - Name: nome do acompanhante.
// - Phone: telefone do acompanhante.
// - Email: email do acompanhante.
// Onde pode ser usada:
// - Em repositórios para buscar, salvar ou atualizar dados de acompanhante.
// -----------------------------------------------------------------------------
namespace GlicareApp.Domain.Entities;

public class Escort
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Relationship { get; set; }
    public string PacientId { get; set; }
    
    public string LoginType { get; set; }
    
    public string TokenAuth { get; set; }
    
    public bool UseTerms { get; set; }
    
}