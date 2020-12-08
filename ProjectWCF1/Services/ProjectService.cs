using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

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
                using (ProjectEntities entities = new ProjectEntities())
                {

                    List<ProjectRoleDto> roleList = new List<ProjectRoleDto>();

                    var ProjectD = (from r in entities.ProjectRoleDto
                                    join u in entities.UserDto
                                    on r.UserId equals u.Id
                                    join p in entities.ProjectDto
                                    on r.ProjectId equals p.Id
                                    where r.ProjectId.Equals(Id)
                                    select new { r.ProjectId, r.UserId, u.UserName, p.ProjectName }).ToList();

                    var role = new ProjectRoleDto();

                    foreach (var item in ProjectD)
                    {
                        var project = new ProjectRoleDto
                        {
                            ProjectId = item.ProjectId,
                            ProjectName = item.ProjectName,
                            UserId = item.UserId,
                            UserName = item.UserName
                        };
                        roleList.Add(project);
                    }
                    return roleList;
                }
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