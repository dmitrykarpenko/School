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
        private ISchoolRepository _schoolRepository;

        public StudentsBL(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public IEnumerable<Student> GetStudents(Expression<Func<Student, bool>> condition, Expression<Func<Student, object>> orderBy, PageInf pageInf)
        {
            return _schoolRepository.GetStudents(condition, orderBy, pageInf);
        }
    }
}
