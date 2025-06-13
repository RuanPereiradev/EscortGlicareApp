namespace DataConnection.Repositories.Interfaces
{
    public interface IGenericRepository<TIn, TOut> where TIn : class where TOut : class
    {
        Task<IEnumerable<TOut>> GetAllAsync();
        Task<TOut> GetByIdAsync(object id);
        Task<object> InsertAsync(TIn entity);
        Task<bool> UpdateAsync(TIn entity);
        Task<bool> DeleteAsync(object id);
        Task<IEnumerable<TOut>> GetItensByFilterAsync(object filter);
    }
}
