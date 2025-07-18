using Dapper;
using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces;
using GlicareApp.Repositories.DataConnection;

namespace GlicareApp.Repositories.Repositories;

public class UserRepository:IUserRepository
{
    private readonly PostgreDbSession _dbSession;

    public UserRepository(PostgreDbSession dbSession)
    {
        _dbSession = dbSession;
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var sql = "SELECT * FROM Users WHERE Id = @Id";
        return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var sql = "SELECT * FROM Users WHERE Email = @Email";
        return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });    }

    public Task<User> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }


    public Task<User> GetUserById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> ForEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(User user)
    {
        var sql = @"
                INSERT INTO Users (Id, Name, Email, PasswordHash, BirthDate, TermsAccepted, RegisterType, LoginType)
                VALUES (@Id, @Name, @Email, @PasswordHash, @BirthDate, @TermsAccepted, @RegisterType, @LoginType)";
            
        await _dbSession.Connection.ExecuteAsync(sql, user);    }
}