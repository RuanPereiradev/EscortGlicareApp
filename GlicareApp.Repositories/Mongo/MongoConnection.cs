using GlicareApp.CrossCuting.Configurations;
using GlicareApp.CrossCuting.Extensions;
using GlicareApp.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace GlicareApp.Repositories.Mongo
{
    public class MongoConnection(IMongoClient mongoClient, IOptions<MongoDbConfiguration> mongoDbSettings, ILogger logger) : IMongoConnection
    {
        private readonly IMongoDatabase _database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        private readonly ILogger _logger = logger;
        public async Task<T> GetDocumentByIdAsync<T>(string collectionName, string id)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq("Id", id);
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return default;
            }
        }
        public async Task<List<T>> GetManyDocumentsFilteredAsync<T>(string collectionName, string filterValue, string filterKey)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq(filterKey, filterValue);
                return await collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return default;
            }
        }
        public async Task<T> GetOneDocumentFilteredAsync<T>(string collectionName, object filterValue, string filterKey)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                CreateIndex(collection, filterKey);
                var filter = Builders<T>.Filter.Eq(filterKey, filterValue);
                return await collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return default;
            }
        }
        public async Task<List<T>> GetManyDocumentsAsync<T>(string collectionName)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                return await collection.Find(new BsonDocument()).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return default;
            }
        }

        public async Task<string> InsertOneDocumentAsync<T>(T document, string collectionName)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                await collection.InsertOneAsync(document);

                var idProperty = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty("_id");

                if (idProperty != null && idProperty.PropertyType == typeof(string))
                {
                    // Retorna o valor do ID, assumindo que ele é um ObjectId
                    return (string)idProperty.GetValue(document)!;
                }
                else
                {
                    throw new InvalidOperationException("O tipo T deve ter uma propriedade Id ou _id do tipo ObjectId.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return default;
            }
        }

        public async Task SetManyDocumentsAsync<T>(IEnumerable<T> documents, string collectionName)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                await collection.InsertManyAsync(documents);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return;
            }
        }

        public async Task UpdateOneDocumentAsync<T>(T document, string collectionName, string id)
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                var filter = Builders<T>.Filter.Eq("Id", id);
                await collection.ReplaceOneAsync(filter, document);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Format());
                return;
            }
        }

        private void CreateIndex<T>(IMongoCollection<T> collection, string index)
        {
            var camposDoIndice = new BsonDocument { { index, 1 } };

            var indiceExiste = collection.Indexes.List().ToList()
                .Any(index =>
                    index.Contains("key") &&
                    index["key"].ToBsonDocument().ToString() == camposDoIndice.ToString());

            if (indiceExiste) return;

            var indexKeysDefinition = Builders<T>.IndexKeys.Ascending(index);
            var indexModel = new CreateIndexModel<T>(indexKeysDefinition);

            // Criar o índice
            var result = collection.Indexes.CreateOne(indexModel);

        }
    }
}
