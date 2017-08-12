using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Model.Helpers
{
    public class PageInf
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public bool IsValid()
        {
            return Page > 0 && PageSize > 0;
        }
    }
}
