using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDBModel.EntityTypes;
using System.Data.SqlClient;
using System.Data;
using DataAccessConnection;
using System.Reflection;

namespace LanguagesSchool.Repositories
{
    public abstract class BaseDataAccess<T> : IBaseRepository<T> where T : EntityBase
    {
        

        protected virtual string TableName { get;  }
        protected virtual string UpdateCommand { get; }
        protected virtual string AddCommand { get; }
        protected abstract T ReadRow(SqlDataReader read);
        protected abstract IList<T> ReadReader(SqlDataReader read);
        protected abstract SqlParameter[] ReturnSqlParamAdd(T entity);
        protected abstract T CompleteEntity(int id, T entity);
        public int Add(T entity)
        {
            string commandText =$"Insert into{TableName} values {AddCommand}; select scope_identity();";
            SqlParameter[] param = ReturnSqlParamAdd(entity);
            var nr = SqlHelper.ExecuteScalar(commandText, param);
            Console.WriteLine("Add "+ Convert.ToInt32(nr));
            return Convert.ToInt32(nr);
        }


        //ok select item by Id
       
        public int Update(int id, T course)
       
        {
            T entity = CompleteEntity(id, course);
            //update command
            string commandText = $"Update {TableName} SET {UpdateCommand} where [Id]=@Id";
            //nu functioneaza parametrii
            
            SqlParameter[] param1 =ReturnSqlParamAdd(entity);
            SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
            parameterId.Value = id;
            SqlParameter[] param = new SqlParameter[7];
            for (int i=0; i<6; i++)
            {
                param[i] = param1[i];
            }
           
            param[6]=parameterId;
            Console.WriteLine(param[6].Value);         
            SqlHelper.ExecuteNonQuery(commandText, param);
            return entity.Id;
        }

        public int Save(T entity)
        {

            int rez;
            if (entity.Id == 0)
                rez = Add(entity);
            else
                rez = Update(entity.Id, entity);

            return rez;
        }
        //ok delete item
        public void Delete(T entity)
        {
                if (entity.Id == 0)
                {
                    return;
                }
                string commandText = $"DELETE FROM {TableName} WHERE [Id] = @Id";
                SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
                parameterId.Value = entity.Id;
                SqlParameter[] param = new SqlParameter[1] { parameterId };
                SqlHelper.ExecuteNonQuery(commandText, param);
                //Console.WriteLine($"S-a sters cartea cu id-ul {obj.Id}");

        }
            
              
        //get all to list-ok
        public IList<T> GetAll()
        {
            IList<T> lista = new List<T>();
            string commandText = $"Select * from {TableName}";
            SqlDataReader rows = SqlHelper.ExecuteReader(commandText);
            lista = ReadReader(rows);
            Console.WriteLine(lista);
            return lista;
        }
        public T GetById(int id)
        {
            string commandText = $"SELECT * FROM {TableName} WHERE [Id] = @Id";
            SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
            parameterId.Value = id;
            SqlParameter[] param = new SqlParameter[1] { parameterId };
            SqlDataReader row = SqlHelper.ExecuteReader(commandText, param);
            return ReadRow(row);
        }

        protected virtual string IdentityCollectionToSqlIdFormat(List<T> collection)
       {
            var array = collection.Select(x => x.Id);
           return string.Join(",", array);
       }
        
    }
}
