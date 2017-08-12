using School.DataLayer.Abstract;
using School.Logic.DTOs;
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
    public class StudentsLogic
    {
        private IUnitOfWork _unitOfWork;

        public StudentsLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Feed<StudentDTO> GetStudentsFeed(Expression<Func<Student, bool>> filter = null, PageInf pageInf = null,
                                                Expression<Func<Student, object>> orderBy = null, bool byDesc = false)
        {
            var students = GetStudents(filter, pageInf, orderBy, byDesc).ToList();
            var studentDTOs = AutoMapper.Mapper.Map<IEnumerable<StudentDTO>>(students); ;

            var feed = new Feed<StudentDTO>() { Collection = studentDTOs };

            if (pageInf != null && pageInf.IsValid())
            {
                feed.Skipped = (pageInf.Page - 1) * pageInf.PageSize;

                int studentsCount = studentDTOs.Count();
                if (studentsCount < pageInf.Page)
                    feed.Count = studentsCount;
            }
            else
            {
                feed.Skipped = 0;
            }

            if (feed.Count == 0)
            {
                var studentsRepo = _unitOfWork.GetRepositiry<Student>();
                feed.Count = studentsRepo.Count(filter);
            }

            return feed;
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
            //preserve groups
            var groupsArr = students.Select(s => s.Group).ToArray();
            //set groups to null to update only students data
            foreach (var student in students)
                student.Group = null;

            var studentsRepo = _unitOfWork.GetRepositiry<Student>();
            studentsRepo.InsertOrUpdate(students);
            _unitOfWork.Save();

            var insOrUpdStudentsArr = students.ToArray();

            //set groups for updated students
            for (int i = 0; i < Math.Max(groupsArr.Length, insOrUpdStudentsArr.Length); ++i)
                insOrUpdStudentsArr[i].Group = groupsArr[i];

            return students;
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
