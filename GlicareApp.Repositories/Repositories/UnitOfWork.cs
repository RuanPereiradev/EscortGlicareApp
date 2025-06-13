using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Repositories.DataConnection;
using Serilog;

namespace DataConnection.Repositories
{
    public class UnitOfWork(PostgreDbSession session, ILogger logger) : IUnitOfWork
    {
        private readonly PostgreDbSession _session = session;
        private readonly ILogger _logger = logger;

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                _session.Transaction.Commit();
            }
            finally
            {
                CloseConnection();
            }
        }
        public void Rollback()
        {
            try
            {
                _session.Transaction.Rollback();
            }
            finally
            {
                CloseConnection();
            }
        }
        public void CloseConnection()
        {
            _session?.Dispose();
        }

        public void CommitOrRollback(bool transactionStatus)
        {
            if (transactionStatus)
                Commit();
            else
                Rollback();
        }
    }
}
