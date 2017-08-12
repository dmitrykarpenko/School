CREATE TABLE [dbo].[Courses]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_dbo.Courses] PRIMARY KEY CLUSTERED ([Id] ASC)
)

;
CREATE TABLE [dbo].[GroupCourses]
(
	[Group_Id] INT NOT NULL , 
    [Course_Id] INT NOT NULL, 
    PRIMARY KEY ([Group_Id], [Course_Id]),
	CONSTRAINT [FK_dbo.GroupCourses_dbo.Groups_Id] FOREIGN KEY ([Group_Id]) REFERENCES [dbo].[Groups] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.GroupCourses_dbo.Courses_Id] FOREIGN KEY ([Course_Id]) REFERENCES [dbo].[Courses] ([Id]) ON DELETE CASCADE
)