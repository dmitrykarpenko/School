using School.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DataLayer.Configurations
{
    public class StudentsConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentsConfiguration()
        {
            Property(p => p.Name).HasMaxLength(100).IsRequired();
            //Property(p => p.GroupId).IsOptional();
        }
    }
}
