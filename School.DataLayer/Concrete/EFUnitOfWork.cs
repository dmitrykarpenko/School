using School.DataLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Model.Entities;
using System.Collections.Concurrent;

namespace School.DataLayer.Concrete
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFSchoolContext _context = new EFSchoolContext();
        private ConcurrentDictionary<Type, object> _repos = new ConcurrentDictionary<Type, object>();

        //all entities which could have a repository
        private static readonly List<Type> _entitiesWithRepos = new List<Type>() { typeof(Student), typeof(Group), typeof(Course) };

        public IRepository<T> GetRepositiry<T>() where T : class, IEntity
        {
            Type repoType = typeof(T);
            if (!_entitiesWithRepos.Contains(repoType))
                throw new ArgumentException("Invalid type: " + repoType.Name);

            object repo = _repos.GetOrAdd(repoType, new BaseEFRepositiry<T>(_context));

            return (IRepository<T>)repo;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        #region dispose
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
