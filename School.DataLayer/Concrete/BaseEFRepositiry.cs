using School.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using School.DataLayer.Abstract;
using School.Model.Helpers;

namespace School.DataLayer.Concrete
{
    public class BaseEFRepositiry<T> : IRepository<T> where T : class, IEntity
    {
        internal EFSchoolContext _context;
        internal DbSet<T> _dbSet;

        public BaseEFRepositiry(EFSchoolContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, PageInf pageInf = null,
                                          Expression<Func<T, object>> include = null,
                                          Expression<Func<T, object>> orderBy = null, bool byDesc = false)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = byDesc ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            if (pageInf != null && pageInf.Page > 1 && pageInf.PageSize > 0)
            {
                int quanToSkip = (pageInf.Page - 1) * pageInf.Page;
                query = query.Skip(quanToSkip).Take(pageInf.PageSize);
            }

            if (include != null)
                query = query.Include(include);

            return query.ToList();
        }

        //public virtual void Insert(T entity)
        //{
        //    _dbSet.Add(entity);
        //}

        //public virtual void Delete(object id)
        //{
        //    T entityToDelete = _dbSet.Find(id);
        //    Delete(entityToDelete);
        //}

        public virtual IEnumerable<T> InsertOrUpdate(IEnumerable<T> entities)
        {
            var ret = new List<T>(entities);
            foreach (var entity in entities)
                if (entity.Id == 0)
                {
                    T addedEntity = _dbSet.Add(entity);
                    //TODO: fill added item id in "ret list"
                }
                else
                {
                    _dbSet.Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                }
            return ret;
        }

        public virtual IEnumerable<T> Delete(int id)
        {
            return Delete(ent => ent.Id == id);
        }

        public virtual IEnumerable<T> Delete(Expression<Func<T, bool>> filter)
        {
            var entitiesToDelete = _dbSet.Where(filter);
            foreach (var entity in entitiesToDelete)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
            return entitiesToDelete;
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }
    }
}
