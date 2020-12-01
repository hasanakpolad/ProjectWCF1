using ProjectWCF1.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWCF1.Unit
{
    public interface IUnitOfWork : IDisposable
    {
        IRepostiroy<T> Repostiroy<T>() where T : class;

        int Save();
    }
}
