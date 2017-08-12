using School.Model.Entities;
using System.Data.Entity.ModelConfiguration;

namespace School.DataLayer.Configurations
{
    public class GroupsConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupsConfiguration()
        {
            Property(p => p.Name).HasMaxLength(100).IsRequired();
        }
    }
}
