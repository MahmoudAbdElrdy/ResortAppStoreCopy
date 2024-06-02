using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using Configuration.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Projects.Dto
{
    public class ProjectDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        [MaxLength(60)]
        public string NameAr { get; set; }
        [MaxLength(60)]
        public string? NameEn { get; set; }
        [MaxLength(60)]
        public string? Code { get; set; }
    
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public long? CompanyId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Project, ProjectDto>()
                          .ReverseMap();
        }
    }
 
    public class ProjectTreeDto
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public bool? IsActive { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }
        public long? ParentId { get; set; }
        public IEnumerable<ProjectTreeDto> children { get; set; }
        public bool expanded { get; set; }
        public long? CompanyId { get; set; }
    }
    public class ProjectSearch : Paging
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }

    }
    public class GetAllProjectTreeCommand
    {
        public string Name { get; set; }
        public Int64? Id { get; set; }
        public Int64? SelectedId { get; set; }
    }
    public class GetLastCode
    {
        public long? ParentId { get; set; }
    }
    public class DeleteProjectCommand
    {
        public long Id { get; set; }
    }
}
