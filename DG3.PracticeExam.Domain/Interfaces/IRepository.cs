using DG3.PracticeExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG3.PracticeExam.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        int Insert(T entity);

        bool Update(T entity);

        bool Delete(int id);

        bool InserBulk(IEnumerable<T> entities);
    }
}
