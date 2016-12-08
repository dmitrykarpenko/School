using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        private StudentsLogic _studentsLogic;
        private GroupsLogic _groupsLogic;
        public StudentController(StudentsLogic studentsLogic, GroupsLogic groupsLogic)
        {
            _studentsLogic = studentsLogic;
            _groupsLogic = groupsLogic;
        }

        //public static readonly JsonSerializerSettings jsonSerSettings = new JsonSerializerSettings()
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    };

        public ActionResult Index()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 20 };

            var students = _studentsLogic.GetStudents(null, pageInf, s => s.Name);
            var availableGroups = _groupsLogic.GetGroups();

            var studentVMs = AutoMapper.Mapper.Map<IEnumerable<StudentVM>>(students);
            var availableGroupVMs = AutoMapper.Mapper.Map<IEnumerable<GroupVM>>(availableGroups);

            var viewModel = new StudentsPageVM() { Students = studentVMs, AvailableGroups = availableGroupVMs, PageInf = pageInf };

            return View(viewModel);
        }

        public JsonResult Save(IEnumerable<StudentVM> studentVMs)
        {
            var students = AutoMapper.Mapper.Map<IEnumerable<Student>>(studentVMs);
            
            var retStudents = _studentsLogic.InsertOrUpdate(students);

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
            var retStudent = _studentsLogic.Delete(id);

            return Json(new { });
        }

        public JsonResult GetPage(PageInf pageInf)
        {
            var students = _studentsLogic.GetStudents(null, pageInf, s => s.Name);

            var studentVMs = AutoMapper.Mapper.Map<IEnumerable<StudentVM>>(students);

            return Json(new { students = studentVMs });
        }
    }
}