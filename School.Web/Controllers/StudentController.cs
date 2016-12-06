using School.Logic;
using School.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Web.Controllers
{
    public class StudentController : Controller
    {
        private StudentsBL _logic;
        public StudentController(StudentsBL logic)
        {
            _logic = logic;
        }
        public ActionResult Index()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 5 };
            var students1 = _logic.GetStudents(s => !s.Name.Contains("4"), s => s.Name, pageInf);
            pageInf.Page = 2;
            var students = _logic.GetStudents(s => !s.Name.Contains("4"), s => s.Name, pageInf);
            return View(students);
        }
    }
}