using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Projects.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configurations.Projects.Repository
{
    public interface IProjectRepository
    {
        Task<List<ProjectTreeDto>> GetTrees(GetAllProjectTreeCommand request);
        Task<string> GetLastCode(GetLastCode request);
        Task<ProjectDto> Create(ProjectDto request);
        Task<ProjectDto> Update(ProjectDto request);
        Task<ProjectDto> DeleteProject(DeleteProjectCommand request);
    }
}
