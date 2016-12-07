using School.Logic;
using School.Model.Entities;
using School.Model.Helpers;
using School.Web.ViewModels;
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
            var pageInf = new PageInf() { Page = 1, PageSize = 20 };
            //var students2 = _logic.GetStudents(s => !s.Name.Contains("4"), pageInf, s => s.Name);
            //pageInf.Page = 2;
            //var students1 = _logic.GetStudents(s => !s.Name.Contains("4"), s => s.Name, pageInf);

            var students = _logic.GetStudents(null, pageInf, s => s.Name);

            var studentVMs = AutoMapper.Mapper.Map<IEnumerable<StudentVM>>(students);

            return View(studentVMs);
        }

        public JsonResult Save(IEnumerable<StudentVM> studentVMs)
        {
            var students = AutoMapper.Mapper.Map<IEnumerable<Student>>(studentVMs);

            //todo: manage nested groups
            var retStudents = _logic.InsertOrUpdate(students);

            var retStudentVMs = AutoMapper.Mapper.Map<IEnumerable<StudentVM>>(retStudents);

            return Json(new { students = retStudentVMs });
        }

        public JsonResult Delete(int id)
        {
            if (id <= 0)
            {
                Response.StatusCode = 400;
                return Json(new { error = "Invalid student id!" });
            }
            var retStudent = _logic.Delete(id);

            return Json(new { });
        }
    }
}