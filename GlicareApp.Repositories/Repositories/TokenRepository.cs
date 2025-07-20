using Dapper;

using GlicareApp.Repositories
using GlicareApp.Domain.Entities;

using GlicareApp.Domain.Interfaces;
using GlicareApp.Repositories.DataConnection;

namespace GlicareApp.Repositories.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly PostgreDbSession _dbSession;

    public TokenRepository(PostgreDbSession dbSession)
    {
        _dbSession = dbSession;
    }
    // Busca o token de verificação usando o e-mail como chave
    public async Task<VerificationToken> ForEmailAsync(string email)
    {
        // SQL para selecionar o token pelo email
        var query = "SELECT * FROM VerificationTokens WHERE Email = @Email";

        // Executa a query usando Dapper e retorna o token correspondente
        return await _dbSession.Connection.QueryFirstOrDefaultAsync<VerificationToken>(query, new { Email = email });
    }

// Busca o token associado ao usuário (usando o email do usuário como referência)
    public async Task<VerificationToken?> GetByEmailAsync(User user)
    {
        // SQL parecida com a anterior, mas usando a propriedade do objeto User
        var query = "SELECT * FROM VerificationTokens WHERE Email = @Email";

        // Executa a consulta passando o email do usuário
        return await _dbSession.Connection.QueryFirstOrDefaultAsync<VerificationToken>(query, new { Email = user.Email });
    }


    public async Task AddAsync(VerificationToken token)
    {
        var query = @"INSERT INTO VerificationTokens (Id, Email, Token, GeneratedAt)
                          VALUES (@Id, @Email, @Token, @GeneratedAt)";
        
            // Executa a query passando os dados do token como parâmetro
            await _dbSession.Connection.ExecuteAsync(query, token);
        
    }
    
    // Método para buscar um token pelo ID
    public async Task<VerificationToken> GetByIdAsync(string id)
    {
        var query = "SELECT * FROM VerificationTokens WHERE Id = @Id";
            
        // Executa a query retornando o primeiro resultado encontrado
        return await _dbSession.Connection.QueryFirstOrDefaultAsync<VerificationToken>(query, new { Id = id });
    }

    // Método para buscar um token pelo email

    public async Task<VerificationToken> GetByEmailAsync(string email)
    {
        var query = @"SELECT * FROM VerificationTokens WHERE Email = @Email";
        
        
        // Executa a query usando o e-mail como parâmetro
        return await _dbSession.Connection.QueryFirstOrDefaultAsync<VerificationToken>(query, new { Email = email });
    }
    
    //Método pra deletar token com base no ID
    public async Task RemoveAsync(string tokenId)
    {
    var query = "DELETE FROM VerificationTokens WHERE Id = @Id";
    
    await _dbSession.Connection.ExecuteAsync(query, new { Id = tokenId });
    
    }

    // Método para atualizar um token existente
    public async Task UpdateAsync(VerificationToken token)
    {
        var query = @"UPDATE VerificationTokens
                          SET Token = @Token, GeneratedAt = @GeneratedAt
                          WHERE Email = @Email";
        
        // Executa a atualização passando os novos dados do token
        await _dbSession.Connection.ExecuteAsync(query, token);
    }
}