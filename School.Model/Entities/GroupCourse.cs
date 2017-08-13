using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Entities
{
    public class GroupCourse
    {
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public virtual Group Group { get; set; }
        public virtual Course Course { get; set; }
    }
}
