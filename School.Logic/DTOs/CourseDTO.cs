﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Logic.DTOs
{
    public class CourseDTO : BaseDTO
    {
        public ICollection<GroupDTO> Groups { get; set; }
    }
}
