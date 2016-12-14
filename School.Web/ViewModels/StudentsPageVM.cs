using School.Model.Helpers;
using System.Collections.Generic;

namespace School.Web.ViewModels
{
    public class StudentsPageVM
    {
        public IEnumerable<StudentVM> Students { get; set; }
        public IEnumerable<GroupVM> AvailableGroups { get; set; }
        public PageInf PageInf { get; set; }
        public int CountOfAllStudents { get; set; }
    }
}
