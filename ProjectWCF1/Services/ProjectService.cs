﻿using ProjectWCF1.Interfaces;
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
                unitOfWork.Repostiroy<ProjectDto>().Add(dto);
                return unitOfWork.Save() > 0;
            }
        }

        public bool AddRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                unitOfWork.Repostiroy<ProjectRoleDto>().Add(dto);
                return unitOfWork.Save() > 0;
            }
        }

        public ProjectDto GetProject(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Repostiroy<ProjectDto>().Get(Id);
            }
        }

        public List<ProjectRoleDto> GetRole(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var role = new ProjectRoleDto();
                role = unitOfWork.Repostiroy<ProjectRoleDto>().Get(Id);
                List<ProjectRoleDto> roleList = new List<ProjectRoleDto>();
                roleList.Add(role);
                return roleList;
            }
        }
    }
}