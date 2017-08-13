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
        
        //public DbSet<Course> Courses { get; set; }
        // public DbSet<GroupCourse> GroupCourses { get; set; } // many-to-many relationship table, not yet created

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EFSchoolContext>(null);

            base.OnModelCreating(modelBuilder);

            // Students

            modelBuilder.Configurations.Add(new StudentsConfiguration());

            // Groups

            var groupsConfig = new GroupsConfiguration();
            groupsConfig.HasMany(g => g.Courses).WithMany(c => c.Groups);
            modelBuilder.Configurations.Add(groupsConfig);

            // Courses

            modelBuilder.Configurations.Add(new CoursesConfiguration());
        }
    }
}
