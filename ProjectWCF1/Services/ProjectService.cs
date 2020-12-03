using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Web;

namespace ProjectWCF1.Services
{
    public class ProjectService : IProjectService
    {
        public bool AddProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (dto.GetType() == typeof(ProjectDto))
                {
                    if (dto != null)
                    {
                        unitOfWork.Repostiroy<ProjectDto>().Add(dto);
                        return unitOfWork.Save() > 0;
                    }
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
        }

        public bool UpdateProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (dto.GetType() == typeof(ProjectDto))
                {
                    ProjectDto project = unitOfWork.Repostiroy<ProjectDto>().Get(dto.Id);
                    if (project != null)
                    {
                        project.ProjectName = dto.ProjectName;

                        unitOfWork.Repostiroy<ProjectDto>().Update(project);
                        return unitOfWork.Save() > 0;

                    }
                    else
                        throw new WebFaultException(HttpStatusCode.NotFound);
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
        }

        public ProjectDto GetProject(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ProjectDto dto = unitOfWork.Repostiroy<ProjectDto>().Get(Id);
                if (dto != null)
                    return unitOfWork.Repostiroy<ProjectDto>().Get(Id);
                else
                    throw new WebFaultException(HttpStatusCode.NotFound);
            }
        }

        public bool DeleteProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ProjectDto project = unitOfWork.Repostiroy<ProjectDto>().Get(dto.Id);
                if (project != null)
                {
                    unitOfWork.Repostiroy<ProjectDto>().Delete(project);
                    return unitOfWork.Save() > 0;
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
            }
        }

        public bool AddRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (dto.GetType() == typeof(ProjectRoleDto))
                {
                    if (dto != null)
                    {
                        unitOfWork.Repostiroy<ProjectRoleDto>().Add(dto);
                        return unitOfWork.Save() > 0;
                    }
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                else
                    throw new WebFaultException(HttpStatusCode.BadRequest);
            }
        }

        public bool UpdateRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ProjectRoleDto roleDto = unitOfWork.Repostiroy<ProjectRoleDto>().Get(dto.Id);
                if (roleDto != null)
                {
                    unitOfWork.Repostiroy<ProjectRoleDto>().Update(dto);
                    return unitOfWork.Save() > 0;
                }
                else
                    throw new WebFaultException(HttpStatusCode.NotFound);
            }
        }

        public List<ProjectRoleDto> GetRole(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ProjectEntities entities = new ProjectEntities();
                var projectQuery = from p in entities.ProjectDto
                                   join r in entities.ProjectRoleDto
                                   on p.Id equals r.ProjectId
                                   select new { p.ProjectName };

                var userQuery = from u in entities.UserDto
                                join r in entities.ProjectRoleDto
                                on u.Id equals r.UserId
                                select new { u.UserName };

                var role = new ProjectRoleDto();
                role = unitOfWork.Repostiroy<ProjectRoleDto>().Get(Id);
                List<ProjectRoleDto> roleList = new List<ProjectRoleDto>();
                role.ProjectName = projectQuery.Select(x => x.ProjectName).ToString();
                role.UserName = userQuery.Select(x => x.UserName).ToString();
                roleList.Add(role);
                return roleList;


            }

        }

        public bool DeleteRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                ProjectRoleDto role = unitOfWork.Repostiroy<ProjectRoleDto>().Get(dto.Id);
                if (role != null)
                {
                    unitOfWork.Repostiroy<ProjectRoleDto>().Delete(role);
                    return unitOfWork.Save() > 0;
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
            }
        }
    }
}