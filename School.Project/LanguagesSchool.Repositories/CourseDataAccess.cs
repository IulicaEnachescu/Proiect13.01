using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SchoolDBModel.EntityTypes;
using DataAccessConnection;
using System.Data;

namespace LanguagesSchool.Repositories
{
    public class CourseDataAccess : BaseDataAccess<Course>, ICourseRepository
    {
        protected override string TableName
        {
            get
            { return "[dbo].[Course]"; }
        }
        protected override string UpdateCommand
        {
            get
            {
                return "[NumberOfLessons]=@NumberOfLessons, [Description]=@Description," +
                    " [Category]=@Category,[Language]=@Language, [Level]=@Level,[StatusActive]=@StatusActive";
            }
        }
        protected override string AddCommand
        {
            get
            {
                return "(@NumberOfLessons, @Description, @Category, @Language, @Level, @StatusActive)";
            }
        }
        public IList<Course> Courses { get; set; } = new List<Course>();
       
        protected override SqlParameter[] ReturnSqlParamAdd(Course entity)
        {
            SqlParameter parameterNumberOfLessons = new SqlParameter("NumberOfLessons", SqlDbType.Int);
            parameterNumberOfLessons.Value = entity.NumberOfLessons;
            SqlParameter parameterDescription = new SqlParameter("Description", SqlDbType.VarChar);
            parameterDescription.Value = entity.Description;
            SqlParameter parameterCategory = new SqlParameter("Category", SqlDbType.VarChar);
            parameterCategory.Value = entity.Category;
            SqlParameter parameterLanguage = new SqlParameter("Language", SqlDbType.VarChar);
            parameterLanguage.Value = entity.Language;
            SqlParameter parameterLevel = new SqlParameter("Level", SqlDbType.VarChar);
            parameterLevel.Value = entity.Level;
            SqlParameter parameterStatusActive = new SqlParameter("StatusActive", SqlDbType.Bit);
            parameterStatusActive.Value = entity.StatusActive;
            SqlParameter[] param = new SqlParameter[6] { parameterNumberOfLessons, parameterDescription, parameterCategory,
            parameterLanguage, parameterLevel,  parameterStatusActive};
            return param;
        }
      
       
        protected override Course CompleteEntity(int id, Course entity)
        {        //complete the object entity from database if has empty fields
                IList<Course> list = GetAll();
                Course copyCourse=list.Where(x => x.Id == id).FirstOrDefault();
                if (copyCourse == null) return null;
                if (string.IsNullOrEmpty(entity.Description)) entity.Description = copyCourse.Description;
                if (object.Equals(entity.Language, null)) entity.Language = copyCourse.Language;
                if (object.Equals(entity.Level, null)) entity.Level = copyCourse.Level;
                if (object.Equals(entity.Category, null)) entity.Category = copyCourse.Category;
                return copyCourse;
            
        }
       /* public override int Update(Course course)

        {
                Course entity = CompleteEntity(course);
                update command
               string commandText = $"Update [dbo].[Course] SET [NumberOfLessons]=@NumberOfLessons, [Description]=@Description, [Category]=@Category,[Language]=@Language, [Level]=@Level,[StatusActive]=@StatusActive where [Id]=@Id";
            SqlParameter parameterNumberOfLessons = new SqlParameter("NumberOfLessons", SqlDbType.Int);
             parameterNumberOfLessons.Value = entity.NumberOfLessons;
             SqlParameter parameterDescription = new SqlParameter("Description", SqlDbType.VarChar);
             parameterDescription.Value = entity.Description;
             SqlParameter parameterCategory = new SqlParameter("Category", SqlDbType.VarChar);
             parameterCategory.Value = entity.Category;
             SqlParameter parameterLanguage = new SqlParameter("Language", SqlDbType.VarChar);
             parameterLanguage.Value = entity.Language;
             SqlParameter parameterLevel = new SqlParameter("Level", SqlDbType.VarChar);
             parameterLevel.Value = entity.Level;
             SqlParameter parameterStatusActive = new SqlParameter("StatusActive", SqlDbType.Bit);
             parameterStatusActive.Value = entity.StatusActive;
             SqlParameter parameterId = new SqlParameter("Id", SqlDbType.Int);
             parameterId.Value = entity.Id;
             SqlParameter[] param = new SqlParameter[7] { parameterNumberOfLessons, parameterDescription, parameterCategory,
         parameterLanguage, parameterLevel,  parameterStatusActive, parameterId};
            SqlParameter[] param = ReturnSqlParam(entity);
                SqlHelper.ExecuteNonQuery(commandText, param);
                return entity.Id;
        }*/
               
       //get data from reader to list
        protected override IList<Course> ReadReader(SqlDataReader read)
        {
            IList<Course> courses = new List<Course>();
            while (read.Read())
            {
                Course course = new Course();          
              var currentRow = read;
                
               course.Id = (int)currentRow["Id"];
                course.NumberOfLessons = (int)currentRow["NumberOfLessons"];
                course.Description = currentRow["Description"].ToString();

                course.Category = (CategoryTypes)Enum.Parse(typeof(CategoryTypes), currentRow["Category"].ToString());
                course.Language = (LanguageTypes)Enum.Parse(typeof(LanguageTypes), currentRow["Language"].ToString());
                course.Level = (LevelTypes)Enum.Parse(typeof(LevelTypes), currentRow["Level"].ToString());
                course.StatusActive = (bool)currentRow["StatusActive"];
                courses.Add(course);
            }
            read.Close();
            return courses;
        }
        //get data from reader for a row
        protected override Course ReadRow(SqlDataReader read)
        {
            
            Course course = new Course();
            while (read.Read())
            {
                var currentRow = read;
               
                course.Id = (int)currentRow["Id"];
                course.NumberOfLessons = (int)currentRow["NumberOfLessons"];
                course.Description = currentRow["Description"].ToString();

                course.Category = (CategoryTypes)Enum.Parse(typeof(CategoryTypes), currentRow["Category"].ToString());
                course.Language = (LanguageTypes)Enum.Parse(typeof(LanguageTypes), currentRow["Language"].ToString());
                course.Level = (LevelTypes)Enum.Parse(typeof(LevelTypes), currentRow["Level"].ToString());
                course.StatusActive = (bool)currentRow["StatusActive"];
                
            }
            read.Close();
            return course;
        }
        //to string for an object course
        public string GetString(Course course)
        {
            StringBuilder str = new StringBuilder();
            str.Append($"Curs: {course.Id} ");
            str.Append($"Description:  {course.Language}, {course.Level}, {course.Category}, " +
                $"{course.Description}, Number of lessons: {course.NumberOfLessons}, Active: {course.StatusActive}");
            return str.ToString();
             }
       //to string for list of course
        public string ToString(List <Course> lista)
        {
            StringBuilder build = new StringBuilder();
            foreach (var item in lista)
            {
                build.Append($"{GetString(item) }\n");
            }
            return build.ToString();
        }

       
    }
}
