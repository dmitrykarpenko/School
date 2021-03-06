﻿using School.Model.Entities;
using System.Data.Entity.ModelConfiguration;

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
