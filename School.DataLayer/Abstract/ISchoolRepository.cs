using School.Model.Entities;
using School.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.DataLayer.Abstract
{
    public interface ISchoolRepository
    {
        IEnumerable<Student> GetStudents(Expression<Func<Student, bool>> wherePredicate,
                                         Expression<Func<Student, object>> orderByPredicate, PageInf pageInf);
        IEnumerable<Student> GetStudents(Expression<Func<Student, bool>> wherePredicate,
                                         Expression<Func<Student, object>> orderByPredicate, bool orderByDesc, PageInf pageInf);
    }
}
