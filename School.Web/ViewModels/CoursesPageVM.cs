using School.Model.Helpers;
using System.Collections.Generic;

namespace School.Web.ViewModels
{
    public class CoursesPageVM
    {
        public IEnumerable<CourseVM> Courses { get; set; }
        public PageInf PageInf { get; set; }
    }
}
