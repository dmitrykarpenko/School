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

        public IEnumerable<Student> GetStudents(Expression<Func<Student, bool>> filter = null, PageInf pageInf = null,
                                                Expression<Func<Student, object>> orderBy = null, bool byDesc = false)
        {
            var studentsRepo = _unitOfWork.GetRepositiry<Student>();

            var students = studentsRepo.Get(filter, pageInf, s => s.Group, orderBy, byDesc);

            return students;
        }

        public virtual IEnumerable<Student> InsertOrUpdate(IEnumerable<Student> students)
        {
            var studentsRepo = _unitOfWork.GetRepositiry<Student>();

            var retStudents = studentsRepo.InsertOrUpdate(students);
            _unitOfWork.Save();

            return retStudents;
        }

        public virtual IEnumerable<Student> Delete(int studentId)
        {
            var studentsRepo = _unitOfWork.GetRepositiry<Student>();

            var retStudents = studentsRepo.Delete(studentId);
            _unitOfWork.Save();

            return retStudents;
        }
    }
}
