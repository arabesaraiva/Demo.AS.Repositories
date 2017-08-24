using Demo.AS.Repositories.Core;
using Demo.AS.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories
{
    public interface IDemoRepositories : IRepositoriesBase, IDisposable
    {
        IRepository<Usuario, int> Usuario { get; }
    }
}
