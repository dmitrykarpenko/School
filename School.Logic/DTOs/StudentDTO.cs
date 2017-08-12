using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Logic.DTOs
{
    public class StudentDTO : BaseDTO
    {
        public GroupDTO Group { get; set; }
    }
}
