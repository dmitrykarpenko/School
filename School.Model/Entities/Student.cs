using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Entities
{
    public class Student : BaseEntity
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
