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

        public ActionResult AddNewOrEdit(int? id)
        {
            CourseVM vm = null;
            IEnumerable<Group> notSelectedGroups = null;

            if (id != null)
            {
                int idVal = id.GetValueOrDefault();
                var course = _coursesLogic.GetCourses(c => c.Id == idVal).FirstOrDefault();
                if (course != null)
                {
                    vm = AutoMapper.Mapper.Map<CourseVM>(course);
                    if (vm.SelectableGroups != null && vm.SelectableGroups.Any())
                    {
                        foreach (var sg in vm.SelectableGroups)
                            sg.Selected = true;

                        var selectedGroupIds = vm.SelectableGroups.Select(sg => sg.Id).ToList();
                        notSelectedGroups = _groupsLogic.GetGroups(g => !selectedGroupIds.Contains(g.Id));
                    }
                }
            }

            if (vm == null)
                vm = new CourseVM() { SelectableGroups = new List<SelectableGroupVM>() };

            if (notSelectedGroups == null)
                notSelectedGroups = _groupsLogic.GetGroups();

            ((List<SelectableGroupVM>)vm.SelectableGroups)
                .AddRange(AutoMapper.Mapper.Map<IEnumerable<SelectableGroupVM>>(notSelectedGroups));

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddNewOrEdit(CourseVM courseVM)
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