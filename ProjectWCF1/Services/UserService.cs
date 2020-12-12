using Newtonsoft.Json;
using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System;
using System.Net;
using System.ServiceModel.Web;

namespace ProjectWCF1.Services
{
    public class UserService : IUserService
    {
        WebOperationContext webOperationContext = WebOperationContext.Current;

        /// <summary>
        /// AddUser metodu SaveUserDto tipinde model alacak ve veri tabanına kayıt edecek
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string olarak userdto ve httpstatus</returns>
        public string AddUser(SaveUserDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    SaveUserDto saveUser = unitOfWork.Repostiroy<SaveUserDto>().Get(dto.Id);
                    if (dto.UserName != null && saveUser == null)
                    {
                        var user = new UserDto()
                        {
                            Name = dto.Name,
                            UserName = dto.UserName
                        };

                        unitOfWork.Repostiroy<SaveUserDto>().Add(dto);
                        unitOfWork.Repostiroy<UserDto>().Add(user);

                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return new WebFaultException<Error>(new Error(200, "İşlem Başarılı", user), HttpStatusCode.OK).ToString();//*
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
                //catch (Exception)
                //{
                //    throw new WebFaultException(HttpStatusCode.BadRequest);
                //}
                //catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                //{
                //    throw new WebFaultException<Error>(new Error(400, dto.GetType().Name + " tipinde değil"), HttpStatusCode.BadRequest);
                //}
                catch (Exception ex)
                {
                    throw new WebFaultException<Error>(new Error(404, "boş değer eklenemez"), HttpStatusCode.NotFound);
                }
            }
        }

        /// <summary>
        /// SaveUserDto modeli alacak eşleşen kayda güncelleme yapacak
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>string model httpStatusCode</returns>
        public string UpdateUser(SaveUserDto dto)
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

                        if (unitOfWork.Save() > 0)
                        {
                            //throw new WebFaultException<Error>(new Error(200, "İşlem Başarılı", userDto), HttpStatusCode.OK);

                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                          //  return JsonConvert.SerializeObject(userDto, Formatting.Indented);
                            // return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userDto)));

                        }
                        else
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                            throw new WebFaultException<Error>(new Error(500, "İşlem Gerçekleşmedi"), HttpStatusCode.InternalServerError);
                        }
                    }
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        return webOperationContext.OutgoingResponse.StatusDescription;
                    }
                    // throw new WebFaultException<Error>(new Error(404, "Model Bulunamadı"), HttpStatusCode.NotFound);
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<Error>(new Error(400, "belirtilen model bulunamadı"), HttpStatusCode.BadRequest);

                }

            }
        }

        /// <summary>
        /// Parametre olarak gelen Id ile eşleşen kayıt dönecek
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>UserDto ve HttpStatusCode</returns>
        public UserDto GetUser(int Id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    UserDto user = unitOfWork.Repostiroy<UserDto>().Get(Id);
                    if (user != null)
                        return unitOfWork.Repostiroy<UserDto>().Get(Id);
                    else
                    {
                        webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<Error>(new Error(404, "'" + Id + "' ile eşleşen kullanıcı bulunamadı"), HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Parametre olarak gelen model ile eşleşen kaydı silecek
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string DeleteUser(SaveUserDto dto)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (dto == null)
                        throw new Exception();

                    SaveUserDto saveUser = unitOfWork.Repostiroy<SaveUserDto>().Get(dto.Id);
                    UserDto user = unitOfWork.Repostiroy<UserDto>().Get(dto.Id);
                    if (saveUser != null)
                    {
                        unitOfWork.Repostiroy<SaveUserDto>().Delete(saveUser);
                        unitOfWork.Repostiroy<UserDto>().Delete(user);

                        if (unitOfWork.Save() > 0)
                        {
                            webOperationContext.OutgoingResponse.StatusCode = HttpStatusCode.OK;
                            return webOperationContext.OutgoingResponse.StatusDescription;
                            //return JsonConvert.SerializeObject(user);
                            //return new WebFaultException<Error>(new Error(200, "Başarılı"), HttpStatusCode.OK).ToString();
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
            }
            catch (Exception ex)
            {
                throw new WebFaultException<Error>(new Error(400, "Model Bulunamadı"), HttpStatusCode.BadRequest);

            }
        }
    }
}
