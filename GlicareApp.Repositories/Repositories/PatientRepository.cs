using System.Data;
using Dapper;
using GlicareApp.Domain.Entities;
using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Repositories.DataConnection;

namespace DataConnection.Repositories;

public class PatientRepository : IPacientRepository
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    public PatientRepository(PostgreDbSession session)
    {
        _connection = session.Connection;
        _transaction = session.Transaction;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        if (!int.TryParse(id, out int intId))
            return false;

        var sql = "SELECT 1 FROM patients WHERE id = @id";
        var result = await _connection.ExecuteScalarAsync<int?>(sql, new { id = intId }, _transaction);
        return result.HasValue;
    }

    public async Task<List<Patient>> GetAllPatientsAsync()
    {
        var sql = "SELECT * FROM patients";
        var result = await _connection.QueryAsync<Patient>(sql, transaction: _transaction);
        return result.AsList();
    }

    public async Task<Patient> GetPatientByIdAsync(string patientId)
    {
        if (!int.TryParse(patientId, out int intId))
            return null;

        var sql = "SELECT * FROM patients WHERE id = @id";
        var patient = await _connection.QueryFirstOrDefaultAsync<Patient>(sql, new { id = intId }, _transaction);
        return patient;
    }
}