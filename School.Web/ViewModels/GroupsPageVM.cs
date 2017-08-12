using School.Model.Helpers;
using System.Collections.Generic;

namespace School.Web.ViewModels
{
    public class GroupsPageVM
    {
        public IEnumerable<GroupVM> Groups { get; set; }
        public PageInf PageInf { get; set; }
        //public int CountOfAllGroups { get; set; }
    }
}
