
    // -----------------------------------------------------------------------------
    // 📄 Classe: EscortRepository
    // 📦 Namespace: GlicareApp.Repositories
    //
    // 🧠 O que é:
    // Implementação do repositório para manipular dados da entidade Escort (acompanhante)
    // no banco PostgreSQL usando Dapper para consultas SQL.
    //
    // 🔧 Pra que serve:
    // - Usar a conexão e transação gerenciadas pelo PostgreDbSession (injeção de dependência).
    // - Executar operações CRUD (Create, Read, Update, Delete) diretamente via SQL.
    // - Manter o controle de transação para garantir consistência dos dados.
    //
    // -----------------------------------------------------------------------------

    using System.Data;
    using Dapper;
    using GlicareApp.Domain.Entities;
    using GlicareApp.Domain.Interfaces.Repositories;
    using GlicareApp.Repositories.DataConnection;

    namespace DataConnection.Repositories;

    public class EscortRepository : IEscortRepository
    {
        private readonly IDbConnection _connection;
        
        private readonly IDbTransaction _transaction;

        public EscortRepository(PostgreDbSession session)
        {
            _connection = session.Connection;
            _transaction = session.Transaction;
        }
        
        
        public async Task<List<Escort>> GetAllEscortsAsync()
        {
            var sql = "SELECT * FROM escorts";
            var result = await _connection.QueryAsync<Escort>(sql, transaction: _transaction);
            return result.ToList();
        }

        public Task<Escort> GetIdAsync(string id)
        {
            var sql = "SELECT * FROM escorts WHERE id = @id";
            return _connection.QueryFirstOrDefaultAsync<Escort>(sql, new { id }, transaction: _transaction);
        }

        public async Task<string> InsertEscortAsync(Escort escort)
        {
            var sql = @"INSERT INTO escorts (name, phone, email, relationship, pacient_id)
                VALUES (@Name, @Phone, @Email, @Relationship, @PacientId)
                RETURNING id"; 
            
            if (!int.TryParse(escort.PacientId, out int pacientIdInt))
                throw new ArgumentException("PacientId inválido. Deve ser um número inteiro.");
            var parameters = new
            {
                escort.Name,
                escort.Phone,
                escort.Email,
                escort.Relationship,
                pacientId = pacientIdInt
            };
            var id = await _connection.ExecuteScalarAsync<string>(sql, parameters, transaction: _transaction);
            return id;
        }

        public async Task<string> UpdateEscortAsync(Escort escort)
        {
            var sql = "UPDATE escorts SET name = @name, phone = @phone, email = @email, relationship = @relationship WHERE id = @id RETURNING id";
            var id = await _connection.ExecuteScalarAsync<string>(sql, escort, transaction: _transaction);
            return id;
        }

        public Task<string> DeleteEscortAsync(string id)
        {
            var sql = "DELETE FROM escorts WHERE id = @id RETURNING id";
            var deletedId = _connection.ExecuteScalarAsync<string>(sql, new { id }, transaction: _transaction);
            return deletedId;
        }

        public Task<Escort?> GetPacientByIdAsync(string pacientId)
        {
            if (!int.TryParse(pacientId, out int pacientIdInt))
                throw new ArgumentException("PacientId inválido. Deve ser um número inteiro.");
            
            var sql = "SELECT * FROM escorts WHERE pacient_id = @pacientId";
            return _connection.QueryFirstOrDefaultAsync<Escort>(sql, new { pacientId = pacientIdInt }, transaction: _transaction);
            
        }
    }