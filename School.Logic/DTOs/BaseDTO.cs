using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Logic.DTOs
{
    public class BaseDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
