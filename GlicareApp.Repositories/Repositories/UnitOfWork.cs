using GlicareApp.Domain.Interfaces.Repositories;
using GlicareApp.Repositories.DataConnection;
using Microsoft.Extensions.Logging;  // Importar ILogger correto

namespace DataConnection.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreDbSession _session;
        private readonly ILogger<UnitOfWork> _logger;

        // Construtor correto (não usar sintaxe de parâmetro no nome da classe)
        public UnitOfWork(PostgreDbSession session, ILogger<UnitOfWork> logger)
        {
            _session = session;
            _logger = logger;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
            _logger.LogInformation("Transaction started.");
        }

        public void Commit()
        {
            try
            {
                _session.Transaction.Commit();
                _logger.LogInformation("Transaction committed.");
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
                _logger.LogInformation("Transaction rolled back.");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void CloseConnection()
        {
            _session?.Dispose();
            _logger.LogInformation("Connection closed.");
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
