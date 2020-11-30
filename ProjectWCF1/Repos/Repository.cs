using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectWCF1.Repos
{
    public class Repository<T> : IRepostiroy<T> where T : class
    {
        private readonly ProjectEntities _context;
        private DbSet<T> _dbSet;
        public Repository(ProjectEntities context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T dto)
        {
            _dbSet.Add(dto);
        }

        public T Get(int Id)
        {
            return _dbSet.Find(Id);
        }
    }
}