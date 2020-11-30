using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWCF1.Services
{
    public class ProjectService : IProjectService
    {
        public bool AddProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.projectRepository.Add(dto);
                return unitOfWork.Save() > 0;
            }
        }

        public bool AddRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.roleRepository.Add(dto);
                return unitOfWork.Save() > 0;
            }
        }

        public ProjectDto GetProject(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.projectRepository.Get(Id);
            }
        }

        public List<ProjectRoleDto> GetRole(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var role = new ProjectRoleDto();
                List<ProjectRoleDto> roleList = new List<ProjectRoleDto>();
                role.Id = Id;
                roleList.Add(role);
                return roleList;
            }
        }
    }
}