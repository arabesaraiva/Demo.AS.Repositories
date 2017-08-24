using Demo.AS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Core
{
    public interface IRepository<T>
        where T : class, new()
    {
        OperationResult<T> Create();
        OperationResult Insert(T value);
        OperationResult Insert(IEnumerable<T> value);
        OperationResult Delete(T value);
        OperationResult Delete(IEnumerable<T> value);
        Task<OperationResult<T>> GetItemAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetList();
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate);
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
    }

    public interface IRepository<T, TKey> : IRepository<T>
        where T : class, IPrimaryKey<TKey>, new()
    {
        OperationResult<T> CreateAttached(TKey keyValue);
        OperationResult DeleteByKey(params TKey[] keyValue);
        Task<OperationResult<T>> GetItemAsync(TKey keyValue);
        ModifiedEntity<T> Update(TKey keyValue);
    }
}
