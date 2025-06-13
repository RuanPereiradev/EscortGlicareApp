namespace GlicareApp.Domain.Interfaces.Repositories
{
    public interface IMongoConnection
    {
        Task<string> InsertOneDocumentAsync<T>(T document, string collectionName);
        Task SetManyDocumentsAsync<T>(IEnumerable<T> documents, string collectionName);
        Task UpdateOneDocumentAsync<T>(T document, string collectionName, string id);
        Task<List<T>> GetManyDocumentsAsync<T>(string collectionName);
        Task<T> GetDocumentByIdAsync<T>(string collectionName, string id);
        Task<List<T>> GetManyDocumentsFilteredAsync<T>(string collectionName, string filterValue, string filterKey);
        Task<T> GetOneDocumentFilteredAsync<T>(string collectionName, object filterValue, string filterKey);
    }
}
