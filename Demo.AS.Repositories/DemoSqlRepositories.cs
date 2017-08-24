
using Demo.AS.Repositories.Core;
using Demo.AS.Repositories.Entities;
using Demo.AS.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories
{
    public class DemoSqlRepositories : SqlRepositoriesBase<DemoContext>, IDemoRepositories, IRepositoriesBase
    {

        #region Tables
        
        private IRepository<Usuario,int> _Usuario;
        public IRepository<Usuario,int> Usuario
        {
            get
            {
                if (_Usuario == null)
                {
                    _Usuario = new SqlRepository<Usuario,int>(this._context);
                }
                return _Usuario;
            }
        }

        #endregion

    }
}
