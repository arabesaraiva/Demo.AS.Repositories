using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Core
{
    public class ModifiedEntity<T> where T : class
    {
        private T _entity;
        private DbContext _context;
        internal ModifiedEntity(DbContext context, T entity)
        {
            _entity = entity;
            _context = context;
        }

        public ModifiedEntity<T> SetProperty<TProperty>(Expression<Func<T, TProperty>> propertySelector, TProperty newValue)
        {
            _context.Entry(_entity).Property(propertySelector).CurrentValue = newValue;
            _context.Entry(_entity).Property(propertySelector).IsModified = true;
            return this;
        }

        public T GetEntity()
        {
            return _entity;
        }

    }
}
