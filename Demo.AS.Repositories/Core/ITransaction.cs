using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AS.Repositories.Core
{
    public interface ITransaction : IDisposable
    {
        void Commit();
    }
}
