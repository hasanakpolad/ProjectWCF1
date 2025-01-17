﻿using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;

namespace ProjectWCF1.Services
{
    public class ProjectService : IProjectService
    {
        WebOperationContext webOperationContext = WebOperationContext.Current;

        /// <summary>
        /// ProjectDto tipinde model alıp kayıt işlemi yapılacak
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string model httpStatusCode</returns>
        public string AddProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    if (dto == null)
                    {
                        unitOfWork.Repostiroy<ProjectDto>().Add(dto);
                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return JsonConvert.SerializeObject(dto);
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                        }
                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }
                }
                catch (Exception)
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
        }

        /// <summary>
        /// Gelen model ile eşleşen kayıtta güncelleme yapılacak
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string model httpStatusCode</returns>
        public string UpdateProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    ProjectDto project = unitOfWork.Repostiroy<ProjectDto>().Get(dto.Id);
                    if (project != null)
                    {
                        project.ProjectName = dto.ProjectName;

                        unitOfWork.Repostiroy<ProjectDto>().Update(project);
                        if (unitOfWork.Save() > 0)
                        {

                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return JsonConvert.SerializeObject(project);
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                        }

                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }
                }
                catch (Exception)
                {
                    webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.BadRequest;

                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }

            }
        }

        /// <summary>
        /// Parametre olarak gelen Id ile eşleşen kayıt dönülecek
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ProjectDto httpStatusCode</returns>
        public ProjectDto GetProject(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    ProjectDto dto = unitOfWork.Repostiroy<ProjectDto>().Get(Id);
                    if (dto != null)
                        return unitOfWork.Repostiroy<ProjectDto>().Get(Id);
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return null;
                    }
                }
                catch (Exception)
                {
                    throw new WebFaultException<Error>(new Error(400, "İstek Hatası"), HttpStatusCode.BadRequest);
                }
            }
        }

        /// <summary>
        /// Parametre olarak gelen model ile eşleşen kaydı silecek
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string model? httpStatusCode</returns>
        public string DeleteProject(ProjectDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    ProjectDto project = unitOfWork.Repostiroy<ProjectDto>().Get(dto.Id);
                    if (project != null)
                    {
                        unitOfWork.Repostiroy<ProjectDto>().Delete(project);
                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            // return JsonConvert.SerializeObject(project);
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                        }
                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }
                }
                catch (Exception)
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
        }

        /// <summary>
        /// ProjectRoleDto tipinde model alıp kayıt işlemi yapacak
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string model httpStatusCode</returns>
        public string AddRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    if (!dto.ToString().Contains(null))
                    {
                        unitOfWork.Repostiroy<ProjectRoleDto>().Add(dto);
                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return JsonConvert.SerializeObject(dto);
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                        }
                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }

                }
                catch (Exception)
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
        }

        /// <summary>
        /// Gelen model ile eşleşen kayıtta güncelleme yapacak
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string model httpStatusCode</returns>
        public string UpdateRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    ProjectRoleDto roleDto = unitOfWork.Repostiroy<ProjectRoleDto>().Get(dto.Id);
                    if (roleDto != null)
                    {
                        roleDto.Id = dto.Id;
                        roleDto.ProjectId = dto.ProjectId;
                        roleDto.UserId = dto.UserId;
                        roleDto.ProjectName = dto.ProjectName;
                        roleDto.UserName = dto.UserName;

                        unitOfWork.Repostiroy<ProjectRoleDto>().Update(roleDto);
                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return JsonConvert.SerializeObject(roleDto);
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                        }
                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }
                }
                catch (Exception)
                {
                    throw new WebFaultException(HttpStatusCode.BadRequest);
                }
            }
        }

        /// <summary>
        /// Parametre olarak aldığı ProjectId ile eşleşen kayıtları ProjectRoleTipinde liste olarak dönecek
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>List model httpStatusCode</returns>
        public IEnumerable<ProjectRoleDto> GetRole(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                using (ProjectEntities entities = new ProjectEntities())
                {
                    try
                    {
                        ProjectRoleDto roleDto = unitOfWork.Repostiroy<ProjectRoleDto>().Get(Id);
                        if (roleDto != null)
                        {

                            var ProjectD = (from r in entities.ProjectRoleDto.ToList()
                                            join u in entities.UserDto.ToList()
                                            on r.UserId equals u.Id
                                            join p in entities.ProjectDto.ToList()
                                            on r.ProjectId equals p.Id
                                            where r.ProjectId.Equals(Id)
                                            select new ProjectRoleDto() { ProjectId = p.Id, UserId = u.Id, ProjectName = p.ProjectName, UserName = u.UserName }).ToList();

                            return ProjectD;
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                            return null;
                            //  throw new WebFaultException<Error>(new Error(404, "'" + Id + "' ile eşleşen kayıt bulunamadı."), HttpStatusCode.NotFound);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw new WebFaultException(HttpStatusCode.BadRequest);
                    }

                }
            }
        }

        /// <summary>
        /// Parametre olarak gelen model ile eşleşen kaydı silecek
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> httpStatusCode</returns>
        public string DeleteRole(ProjectRoleDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    ProjectRoleDto role = unitOfWork.Repostiroy<ProjectRoleDto>().Get(dto.Id);
                    if (role != null)
                    {
                        unitOfWork.Repostiroy<ProjectRoleDto>().Delete(role);
                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return JsonConvert.SerializeObject(role);
                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                        }
                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }
                }
                catch (Exception)
                {

                    throw new WebFaultException<Error>(new Error(400, "İstek Hatası"), HttpStatusCode.BadRequest);
                }
            }
        }
    }
}