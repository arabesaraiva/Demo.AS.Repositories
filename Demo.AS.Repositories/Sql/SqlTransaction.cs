using Demo.AS.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Sql
{
    public class SqlTransaction : ITransaction
    {

        bool isCommitted = false;
        private System.Transactions.TransactionScope _transaction;

        private IRepositoriesBase _db;
        private DbContext _context;
        public SqlTransaction(DbContext context, IRepositoriesBase db)
        {
            this._transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, new System.Transactions.TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }, System.Transactions.EnterpriseServicesInteropOption.Automatic);
            this._db = db;
            this._context = context;
        }

        public void Commit()
        {
            if (_transaction != null && !isCommitted) this._transaction.Complete();

            isCommitted = true;
        }

        public void Dispose()
        {
            if (_db != null && !isCommitted) { _db.Rollback(); }

            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction = null;
            }
        }

    }

}
