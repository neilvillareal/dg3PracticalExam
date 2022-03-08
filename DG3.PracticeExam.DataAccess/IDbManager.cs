using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG3.PracticeExam.DataAccess
{
    public interface IDbManager
    {
        void AssignStoredProc(string spName);
        
        void SetInputParams(string paramName, DbType type, object value);
        
        void SetOutputParams(string paramName, DbType type);
        
        object GetOutputParams(string paramName);
        
        bool RunNonQuery();
        
        DataSet RunQuery();
        
        void CreateTransaction();
        
        void CommitTransaction();
        
        void RollbackTransaction();

    }
}
