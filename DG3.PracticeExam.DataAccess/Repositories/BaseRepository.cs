using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG3.PracticeExam.DataAccess.Repositories
{
    public abstract class BaseRepository<T>
    {
        public abstract T SetData(DataRow dr);
    }
}
