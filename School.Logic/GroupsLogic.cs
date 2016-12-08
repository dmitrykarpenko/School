using School.DataLayer.Abstract;
using School.Model.Entities;
using School.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Logic
{
    public class GroupsLogic
    {
        private IUnitOfWork _unitOfWork;

        public GroupsLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Group> GetGroups(Expression<Func<Group, bool>> filter = null, PageInf pageInf = null,
                                            Expression<Func<Group, object>> orderBy = null, bool byDesc = false)
        {
            var groupsRepo = _unitOfWork.GetRepositiry<Group>();

            var groups = groupsRepo.Get(filter, pageInf, null, orderBy, byDesc);

            return groups;
        }

        public virtual IEnumerable<Group> InsertOrUpdate(IEnumerable<Group> groups)
        {
            var groupsRepo = _unitOfWork.GetRepositiry<Group>();
            var insOrUpdGroups = groupsRepo.InsertOrUpdate(groups);
            _unitOfWork.Save();

            return insOrUpdGroups;
        }

        public virtual IEnumerable<Group> Delete(int groupId)
        {
            var groupsRepo = _unitOfWork.GetRepositiry<Group>();
            var retGroups = groupsRepo.Delete(groupId);
            _unitOfWork.Save();

            return retGroups;
        }
    }
}
