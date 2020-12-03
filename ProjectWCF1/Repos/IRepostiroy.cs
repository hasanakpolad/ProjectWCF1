using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWCF1.Repos
{
    public interface IRepostiroy<T> where T : class
    {
        void Add(T dto);

        void Update(T dto);

        T Get(int id);

        void Delete(T dto);
    }
}
