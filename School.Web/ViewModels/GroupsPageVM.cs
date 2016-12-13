using School.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Web.ViewModels
{
    public class GroupsPageVM
    {
        public IEnumerable<GroupVM> Groups { get; set; }
        public PageInf PageInf { get; set; }
        //public int CountOfAllGroups { get; set; }
    }
}
