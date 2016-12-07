using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Web.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupVM Group { get; set; }
    }
}
