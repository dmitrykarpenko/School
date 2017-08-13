using School.DataLayer.Configurations;
using School.Model.Entities;
using System.Data.Entity;
using MC = System.Data.Entity.ModelConfiguration;

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

            modelBuilder.Configurations.Add(new GroupsConfiguration());

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Courses).WithMany(c => c.Groups)
                .Map(x =>
                {
                    x.MapLeftKey("GroupId");
                    x.MapRightKey("CourseId");
                    x.ToTable("GroupCourses");
                });

            // Courses

            modelBuilder.Configurations.Add(new CoursesConfiguration());

            //// GroupCourses
            ////EntityTypeConfiguration<Group>

            //var groupCoursesConfig = new MC.EntityTypeConfiguration<GroupCourse>()
            //    .HasRequired(gc => gc.Group).WithMany(g => g.Courses).HasForeignKey(g => g.CourseId);

            //modelBuilder.Configurations.Add();
        }
    }
}
