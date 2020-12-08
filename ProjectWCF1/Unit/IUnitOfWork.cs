using ProjectWCF1.Repos;
using System;

namespace ProjectWCF1.Unit
{
    public interface IUnitOfWork : IDisposable
    {
        IRepostiroy<T> Repostiroy<T>() where T : class;

        int Save();
    }
}
