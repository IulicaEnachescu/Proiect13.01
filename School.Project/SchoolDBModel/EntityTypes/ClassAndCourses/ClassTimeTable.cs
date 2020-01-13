using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class ClassTimeTable:EntityBase
    {
        public int ClassId { get; set; }
        public int LessonNumber { get; set; }
        public DateTime ClassDate { get; set; }

       // public IList <ClassTimeTable> ClassTimeTables { get; set; } = new List<ClassTimeTable>();


    }
}
