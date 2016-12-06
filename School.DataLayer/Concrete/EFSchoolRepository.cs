using School.DataLayer.Abstract;
using School.Model.Entities;
using School.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.DataLayer.Concrete
{
    public class EFSchoolRepository : ISchoolRepository
    {
        EFSchoolContext _context;
        public EFSchoolRepository()
        {
            _context = new EFSchoolContext();
        }

        public IEnumerable<Student> GetStudents(Expression<Func<Student, bool>> condition, Expression<Func<Student, object>> orderBy, PageInf pageInf)
        {
            int quanToSkip = pageInf != null && pageInf.Page > 1 && pageInf.PageSize > 0 ? (pageInf.Page - 1) * pageInf.Page : 0;

            var students = _context.Students.Where(condition).OrderBy(orderBy)
                                   .Skip(quanToSkip).Take(pageInf.PageSize).Include(s => s.Group).ToList();
            return students;
        }
    }
}
