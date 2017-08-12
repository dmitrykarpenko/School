using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Entities
{
    public class Course : BaseEntity
    {
        public ICollection<Group> Groups { get; set; }
    }
}
