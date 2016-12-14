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
    public class GroupController : Controller
    {
        private GroupsLogic _groupsLogic;
        public GroupController(GroupsLogic groupsLogic)
        {
            _groupsLogic = groupsLogic;
        }

        public ActionResult ShowAll()
        {
            var pageInf = new PageInf() { Page = 1, PageSize = 10 };

            var groups = _groupsLogic.GetGroups(null, pageInf, s => s.Name);

            var groupVMs = AutoMapper.Mapper.Map<IEnumerable<GroupVM>>(groups);

            var viewModel = new GroupsPageVM()
            {
                Groups = groupVMs,
                PageInf = pageInf
            };

            return View(viewModel);
        }

        public JsonResult Save(IEnumerable<GroupVM> groupVMs)
        {
            var groups = AutoMapper.Mapper.Map<IEnumerable<Group>>(groupVMs);
            
            _groupsLogic.InsertOrUpdate(groups);

            groupVMs = AutoMapper.Mapper.Map<IEnumerable<GroupVM>>(groups);

            return Json(new { groups = groupVMs });
        }

        public JsonResult Delete(int id)
        {
            if (id <= 0)
            {
                Response.StatusCode = 400;
                return Json(new { error = "Invalid group id!" });
            }
            var retStudent = _groupsLogic.Delete(id);

            return Json(new { });
        }

        public JsonResult GetPage(PageInf pageInf)
        {
            var groups = _groupsLogic.GetGroups(null, pageInf, s => s.Name);

            var groupVMs = AutoMapper.Mapper.Map<IEnumerable<GroupVM>>(groups);

            var viewModel = new GroupsPageVM()
            {
                Groups = groupVMs,
                PageInf = pageInf
            };

            return Json(viewModel);
        }
    }
}