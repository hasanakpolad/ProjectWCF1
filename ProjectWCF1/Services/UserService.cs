using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System;
using System.Net;
using System.ServiceModel.Web;

namespace ProjectWCF1.Services
{
    public class UserService : IUserService
    {
        public bool AddUser(SaveUserDto dto)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (dto == null)
                        throw new Exception("Model boş");
                    var user = new UserDto()
                    {
                        Name = dto.Name,
                        UserName = dto.UserName
                    };

                    unitOfWork.Repostiroy<SaveUserDto>().Add(dto);
                    unitOfWork.Repostiroy<UserDto>().Add(user);
                    return unitOfWork.Save() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }
        }


        public bool UpdateUser(SaveUserDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
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
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Not Found"))
                        throw new WebFaultException(HttpStatusCode.NotFound);

                    throw new WebFaultException(HttpStatusCode.BadRequest);

                }
            }
        }

        public UserDto GetUser(int Id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    UserDto user = unitOfWork.Repostiroy<UserDto>().Get(Id);
                    if (user == null || Id == 0)
                        throw new WebFaultException(HttpStatusCode.NotFound);
                    else
                        return unitOfWork.Repostiroy<UserDto>().Get(Id);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Not Found"))
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                throw new WebFaultException(HttpStatusCode.BadRequest);
            }
        }

        public bool DeleteUser(SaveUserDto dto)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (dto == null)
                        throw new Exception("Model boş");

                    SaveUserDto saveUser = unitOfWork.Repostiroy<SaveUserDto>().Get(dto.Id);
                    UserDto user = unitOfWork.Repostiroy<UserDto>().Get(dto.Id);
                    unitOfWork.Repostiroy<SaveUserDto>().Delete(saveUser);
                    unitOfWork.Repostiroy<UserDto>().Delete(user);
                    return unitOfWork.Save() > 0;

                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                if (ex.Message.Contains("Not Found"))
                {
                    throw new System.ServiceModel.FaultException(ex.Message);
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                throw new WebFaultException(HttpStatusCode.BadRequest);

            }
        }
    }
}
