﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes.ClassAndCourses
{
    class StudentClass:EntityBase
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }
        
    }
}
