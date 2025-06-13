using Dapper;
using DataConnection.Repositories.Interfaces;
using GlicareApp.Repositories.DataConnection;
using System.Data;
using System.Dynamic;
using static Dapper.SqlMapper;

namespace GlicareApp.Repositories.Repositories
{
    public class GenericRepository<TIn, TOut>(PostgreDbSession dbSession, string tableName) : IGenericRepository<TIn, TOut> where TIn : class where TOut : class
    {
        private readonly PostgreDbSession _dbConnection = dbSession;
        private readonly string _tableName = tableName;
        public async Task<IEnumerable<TOut>> GetItensByFilterAsync(object filter)
        {
            var filters = GetFilters(filter);

            var query = $"SELECT * FROM {_tableName} WHERE {filters.Item1} Active = true";

            var result = await _dbConnection.Connection.QueryAsync<TOut>(query, filters.Item2);

            return result;
        }
        public async Task<TOut> GetByIdAsync(object id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id and Active = true";

            var result = await _dbConnection.Connection.QueryFirstOrDefaultAsync<TOut>(query, new { Id = id });

            return result;
        }

        public async Task<IEnumerable<TOut>> GetAllAsync()
        {
            var query = $"SELECT * FROM {_tableName} WHERE Active = true";

            var result = await _dbConnection.Connection.QueryAsync<TOut>(query);

            return result;
        }

        public async Task<object> InsertAsync(TIn entity)
        {
            var query = $"INSERT INTO {_tableName} ({GetColumns()}) VALUES ({GetParameters()}) returning Id";

            var result = await _dbConnection.Connection.ExecuteScalarAsync<object>(query, entity);

            return result;
        }

        public async Task<bool> UpdateAsync(TIn entity)
        {
            var query = $"UPDATE {_tableName} SET {GetUpdateParameters()} WHERE Id = @Id";

            var result = await _dbConnection.Connection.ExecuteAsync(query, entity) > 0;

            return result;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var query = $"UPDATE {_tableName} SET Active = false WHERE Id = @Id";

            var result = await _dbConnection.Connection.ExecuteAsync(query, id) > 0;

            return result;
        }

        private string GetColumns()
        {
            var properties = typeof(TIn).GetProperties();
            return string.Join(", ", properties.Where(p => p.Name != "Id").Select(p => p.Name));
        }

        private string GetParameters()
        {
            var properties = typeof(TIn).GetProperties();
            return string.Join(", ", properties.Where(p => p.Name != "Id").Select(p => "@" + p.Name));
        }

        private string GetUpdateParameters()
        {
            var properties = typeof(TIn).GetProperties();
            return string.Join(", ", properties.Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));
        }

        private (string, object) GetFilters(object filter)
        {
            var properties = filter.GetType().GetProperties();

            var whereClause = string.Join(" AND ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            var parameters = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in properties)
            {
                parameters[property.Name] = property.GetValue(filter);
            }

            return (whereClause, parameters);
        }

    }
}
