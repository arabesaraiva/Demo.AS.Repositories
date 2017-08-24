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
    public class SqlRepository<T, TKey> : SqlRepository<T>, IRepository<T, TKey>
      where T : class, IPrimaryKey<TKey>, new()
    {

        public SqlRepository(DbContext context) : base(context)
        {

        }

        public ModifiedEntity<T> Update(TKey keyValue)
        {
            try
            {
                var updatingEntity = new T() { Id = keyValue };
                _set.Attach(updatingEntity);

                return new ModifiedEntity<T>(_context, updatingEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar do registro no repositório.", ex);
            }
        }

        public OperationResult<T> CreateAttached(TKey keyValue)
        {
            try
            {
                var value = new T() { Id = keyValue };
                _set.Attach(value);

                return new OperationResult<T>() { Data = value };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>() { MainException = new Exception("Erro ao criar instância do tipo.", ex) };
            }
        }

        public OperationResult DeleteByKey(params TKey[] keyValue)
        {
            try
            {
                if (keyValue == null || !keyValue.Any())
                {
                    return new OperationResult();
                }

                keyValue.ToList().
                    ForEach(v =>
                    {
                        var value = this.CreateAttached(v).Data;

                        _set.Remove(value);
                    });

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao remover a instância do contexto pela chave primária.", ex) };
            }
        }

        public async Task<OperationResult<T>> GetItemAsync(TKey keyValue)
        {
            try
            {
                var value = await _set.FindAsync(keyValue);

                return new OperationResult<T>() { Data = value };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>() { MainException = new Exception("Erro ao localizar o registro pela chave primária.", ex) };
            }
        }
    }

    public class SqlRepository<T> : IRepository<T>
       where T : class, new()
    {

        protected DbContext _context;
        protected DbSet<T> _set;

        public SqlRepository(DbContext context)
        {
            this._context = context;
            this._set = context.Set<T>();
        }

        public OperationResult<T> Create()
        {
            try
            {
                var value = _set.Create();

                return new OperationResult<T>() { Data = value };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>() { MainException = new Exception("Erro ao criar instância do tipo.", ex) };
            }
        }

        public OperationResult Delete(T value)
        {
            try
            {
                _set.Remove(value);

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao remover a instância do contexto.", ex) };
            }
        }

        public OperationResult Delete(IEnumerable<T> value)
        {
            try
            {
                _set.RemoveRange(value);

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao remover a instância do contexto.", ex) };
            }
        }

        public async Task<OperationResult<T>> GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var value = await _set.FirstOrDefaultAsync(predicate);


                return new OperationResult<T>() { Data = value };
            }
            catch (Exception ex)
            {
                return new OperationResult<T>() { MainException = new Exception("Erro ao localizar o registro.", ex) };
            }
        }

        public IQueryable<T> GetList()
        {
            return _set;
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }

        public OperationResult Insert(T value)
        {
            try
            {
                _set.Add(value);

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao inserir a instância no contexto.", ex) };
            }
        }

        public OperationResult Insert(IEnumerable<T> value)
        {
            try
            {
                _set.AddRange(value);

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult() { MainException = new Exception("Erro ao inserir a instância no contexto.", ex) };
            }
        }

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return _set.Include(path);
        }

    }
}
