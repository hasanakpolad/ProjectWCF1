using ProjectWCF1.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWCF1.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProjectEntities _context = new ProjectEntities();

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepostiroy<T> Repostiroy<T>() where T : class
        {
            return new Repository<T>(_context);
        }
    }
}