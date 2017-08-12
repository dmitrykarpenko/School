using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Logic.DTOs
{
    public class Feed<T> where T : IDTO
    {
        public IEnumerable<T> Collection { get; set; }
        public int Count { get; set; }
        public int Skipped { get; set; }
    }
}
