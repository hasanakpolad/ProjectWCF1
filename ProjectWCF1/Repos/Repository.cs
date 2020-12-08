using System.Data.Entity;

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

        public void Update(T dto)
        {
            _dbSet.Attach(dto);
            _context.Entry(dto).State = EntityState.Modified;
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Delete(T dto)
        {
            _dbSet.Remove(dto);
        }
    }
}