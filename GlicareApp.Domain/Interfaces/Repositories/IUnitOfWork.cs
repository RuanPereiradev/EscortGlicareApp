namespace GlicareApp.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CommitOrRollback(bool transactionStatus);
        void CloseConnection();
    }
}
