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
    public class UserService : IUserService
    {
        public bool AddUser(SaveUserDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                if (dto.GetType() == typeof(SaveUserDto))
                {
                    if (dto.Name != null)
                    {
                        var user = new UserDto()
                        {
                            Name = dto.Name,
                            UserName = dto.UserName
                        };

                        unitOfWork.Repostiroy<SaveUserDto>().Add(dto);
                        unitOfWork.Repostiroy<UserDto>().Add(user);
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

        public bool UpdateUser(SaveUserDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (dto.GetType() == typeof(SaveUserDto))
                {
                    SaveUserDto saveUser = unitOfWork.Repostiroy<SaveUserDto>().Get(dto.Id);
                    UserDto userDto = unitOfWork.Repostiroy<UserDto>().Get(dto.Id);
                    if (saveUser != null)
                    {

                        userDto.Name = dto.Name;
                        userDto.UserName = dto.UserName;

                        saveUser.Name = dto.Name;
                        saveUser.Password = dto.Password;
                        saveUser.UserName = dto.UserName;


                        unitOfWork.Repostiroy<SaveUserDto>().Update(saveUser);
                        unitOfWork.Repostiroy<UserDto>().Update(userDto);

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

        public UserDto GetUser(int Id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                UserDto user = unitOfWork.Repostiroy<UserDto>().Get(Id);
                if (user != null)
                {
                    return unitOfWork.Repostiroy<UserDto>().Get(Id);
                }
                else
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
            }
        }

        public bool DeleteUser(SaveUserDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                SaveUserDto saveUser = unitOfWork.Repostiroy<SaveUserDto>().Get(dto.Id);
                UserDto user = unitOfWork.Repostiroy<UserDto>().Get(dto.Id);
                if (user != null)
                {
                    unitOfWork.Repostiroy<SaveUserDto>().Delete(saveUser);
                    unitOfWork.Repostiroy<UserDto>().Delete(user);
                    return unitOfWork.Save() > 0;
                }
                else
                    throw new WebFaultException(HttpStatusCode.NotFound);

            }
        }

    }
}