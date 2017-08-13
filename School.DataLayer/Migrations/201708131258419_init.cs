namespace School.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.GroupCourses",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupId, t.CourseId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.GroupCourses", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupCourses", new[] { "CourseId" });
            DropIndex("dbo.GroupCourses", new[] { "GroupId" });
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropTable("dbo.GroupCourses");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Groups");
        }
    }
}
