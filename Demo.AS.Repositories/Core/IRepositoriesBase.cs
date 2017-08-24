using Demo.AS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Core
{
    public interface IRepositoriesBase
    {
        Task<OperationResult> CommitAsync();
        OperationResult Commit();
        OperationResult Rollback();
        ITransaction BeginTransaction();
        Task<T> LoadNavigationPropertyAsync<T, TElement>(T entity, Expression<Func<T, ICollection<TElement>>> selector) where T : class where TElement : class;
        Task<T> LoadNavigationPropertyAsync<T, TProperty>(T entity, Expression<Func<T, TProperty>> selector) where T : class where TProperty : class;
        void ForceChangeProperty<T, TProperty>(T entity, Expression<Func<T, TProperty>> propertySelector, TProperty newValue) where T : class;
    }
}
