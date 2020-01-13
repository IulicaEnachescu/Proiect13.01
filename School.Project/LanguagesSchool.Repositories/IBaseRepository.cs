using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SchoolDBModel.EntityTypes;

namespace LanguagesSchool.Repositories
{
    interface IBaseRepository<T>
    {

        T GetById(int id);
        IList<T> GetAll();
        
        int Save(T entity);
     
        int Update(int id, T entity);
        void Delete(T entity);
     
    }
}