using School.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DataLayer.Concrete
{
    public class EFSchoolContext : DbContext
    {
        public EFSchoolContext() : base("DefaultConnection")
        {
            //Database.SetInitializer<EFSchoolContext>(null);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
