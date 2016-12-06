using School.DataLayer.Abstract;
using School.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DataLayer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepositiry<T>() where T : class, IEntity;
        void Save();
    }
}
