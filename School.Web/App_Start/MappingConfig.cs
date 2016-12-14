﻿using School.Logic.DTOs;
using School.Model.Entities;
using School.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Web
{
    public static class MappingConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Group, GroupVM>();
                config.CreateMap<GroupVM, Group>();
                config.CreateMap<Course, CourseVM>();
                config.CreateMap<CourseVM, Course>();
                config.CreateMap<Student, StudentVM>();
                config.CreateMap<StudentVM, Student>()
                      .ForMember(dest => dest.GroupId,
                                 opt => opt.MapFrom(src => src.Group != null & src.Group.Id != 0 ?
                                                    (int?)src.Group.Id : null));

                config.CreateMap<Group, GroupDTO>();
                config.CreateMap<Course, CourseDTO>();
                config.CreateMap<Student, StudentDTO>();

                config.CreateMap<GroupDTO, GroupVM>();
                config.CreateMap<CourseDTO, CourseVM>();
                config.CreateMap<StudentDTO, StudentVM>();
            });
        }
    }
}
