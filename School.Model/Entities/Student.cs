using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Entities
{
    public class Student : BaseEntity
    {
        public int? GroupId { get; set; } = null;
        public virtual Group Group { get; set; }
    }
}
