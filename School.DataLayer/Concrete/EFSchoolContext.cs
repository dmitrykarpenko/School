using School.DataLayer.Configurations;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new StudentsConfiguration());

            var groupsConfig = new GroupsConfiguration();
            groupsConfig.HasMany(g => g.Courses).WithMany(c => c.Groups);
            modelBuilder.Configurations.Add(groupsConfig);

            modelBuilder.Configurations.Add(new CoursesConfiguration());
        }
    }
}
