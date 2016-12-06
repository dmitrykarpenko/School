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
    public class StudentsBL
    {
        private IUnitOfWork _unitOfWork;

        public StudentsBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Student> GetStudents(Expression<Func<Student, bool>> condition, Expression<Func<Student, object>> orderByDesc, PageInf pageInf)
        {
            var studentsRepo = _unitOfWork.GetRepositiry<Student>();
            var students = studentsRepo.Get(condition, pageInf, s => s.Group, orderByDesc, true);
            return students;
        }
    }
}
