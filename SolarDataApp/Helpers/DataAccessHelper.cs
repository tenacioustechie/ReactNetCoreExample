using System;
using System.Transactions;
using MySql.Data.MySqlClient;

namespace SolarDataApp.Helpers
{

    public interface IDataAccessHelper
    {
        MySqlConnection CreateConnection();
        TransactionScope CreateTransactionScope();
    }

    public class DataAccessHelper : IDataAccessHelper
    {
        private string _connectionString;

        public DataAccessHelper(IConfigProvider configProvider)
        {
            this._connectionString = configProvider.ConnectionString;
            if (this._connectionString == null) {
                throw new ArgumentNullException("Connection string cannot be null");
            }
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public TransactionScope CreateTransactionScope()
        {
            var transactionOptions = new TransactionOptions {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout,
            };

            return new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
