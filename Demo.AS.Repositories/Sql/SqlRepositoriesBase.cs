using Demo.AS.Repositories.Core;
using Demo.AS.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Sql
{
    public abstract class SqlRepositoriesBase<TContext> : IRepositoriesBase, IDisposable
        where TContext : DbContext, new()
    {

        protected TContext _context;
        public SqlRepositoriesBase()
        {
            this._context = (TContext)typeof(TContext).GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        public void ForceChangeProperty<T, TProperty>(T entity, Expression<Func<T, TProperty>> propertySelector, TProperty newValue)
            where T : class
        {
            _context.Entry(entity).Property(propertySelector).CurrentValue = newValue;
            _context.Entry(entity).Property(propertySelector).IsModified = true;
        }

        public OperationResult Commit()
        {
            try
            {
                _context.SaveChanges();

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao aplicar as alterações no banco de dados.", ex) };
            }
        }

        public async Task<OperationResult> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao aplicar as alterações no banco de dados.", ex) };
            }
        }

        public OperationResult Rollback()
        {
            try
            {
                foreach (var entry in this._context.ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            {
                                entry.CurrentValues.SetValues(entry.OriginalValues);
                                entry.State = EntityState.Unchanged;
                                break;
                            }
                        case EntityState.Deleted:
                            {
                                entry.State = EntityState.Unchanged;
                                break;
                            }
                        case EntityState.Added:
                            {
                                entry.State = EntityState.Detached;
                                break;
                            }
                    }
                }

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao cancelar as alterações no banco de dados.", ex) };
            }
        }

        public ITransaction BeginTransaction()
        {
            return new SqlTransaction(this._context, this);
        }

        public async Task<T> LoadNavigationPropertyAsync<T, TElement>(T entity, Expression<Func<T, ICollection<TElement>>> selector)
            where T : class
            where TElement : class
        {
            await _context.Entry(entity).Collection(selector).LoadAsync();

            return entity;
        }

        public async Task<T> LoadNavigationPropertyAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> selector)
            where T : class
            where TProperty : class
        {
            await _context.Entry(entity).Reference(selector).LoadAsync();

            return entity;
        }

        public void Dispose()
        {
            if (this._context != null)
            {
                this.Rollback();

                this._context.Dispose();
                this._context = null;
            }
        }

    }
}
