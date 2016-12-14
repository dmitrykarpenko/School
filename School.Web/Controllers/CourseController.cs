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
    public class CourseController : Controller
    {
        private CoursesLogic _coursesLogic;
        private GroupsLogic _groupsLogic;
        public CourseController(CoursesLogic coursesLogic, GroupsLogic groupsLogic)
        {
            _coursesLogic = coursesLogic;
            _groupsLogic = groupsLogic;
        }

        public ActionResult ShowAll()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 10 };

            var courses = _coursesLogic.GetCourses(null, pageInf, s => s.Name);

            var courseVMs = AutoMapper.Mapper.Map<IEnumerable<CourseVM>>(courses);

            var viewModel = new CoursesPageVM()
            {
                Courses = courseVMs,
                PageInf = pageInf
            };

            return View(viewModel);
        }

        public ActionResult AddNew()
        {
            var availableGroups = _groupsLogic.GetGroups();
            IEnumerable<SelectableGroupVM> availableSelectableGroupVMs = AutoMapper.Mapper.Map<IEnumerable<SelectableGroupVM>>(availableGroups);

            var vm = new CourseVM() { SelectableGroups = availableSelectableGroupVMs };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddNew(CourseVM courseVM)
        {
            var course = AutoMapper.Mapper.Map<Course>(courseVM);

            _coursesLogic.InsertOrUpdate(new List<Course>() { course });

            return RedirectToAction("ShowAll");
        }

        public JsonResult Save(IEnumerable<CourseVM> courseVMs)
        {
            var courses = AutoMapper.Mapper.Map<IEnumerable<Course>>(courseVMs);
            
            _coursesLogic.InsertOrUpdate(courses);

            courseVMs = AutoMapper.Mapper.Map<IEnumerable<CourseVM>>(courses);

            return Json(new { courses = courseVMs });
        }

        public JsonResult Delete(int id)
        {
            if (id <= 0)
            {
                Response.StatusCode = 400;
                return Json(new { error = "Invalid course id!" });
            }
            var retStudent = _coursesLogic.Delete(id);

            return Json(new { });
        }

        public JsonResult GetPage(PageInf pageInf)
        {
            var courses = _coursesLogic.GetCourses(null, pageInf, s => s.Name);

            var courseVMs = AutoMapper.Mapper.Map<IEnumerable<CourseVM>>(courses);

            var viewModel = new CoursesPageVM()
            {
                Courses = courseVMs,
                PageInf = pageInf
            };

            return Json(viewModel);
        }
    }
}