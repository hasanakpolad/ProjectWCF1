using ProjectWCF1.Interfaces;
using ProjectWCF1.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWCF1.Services
{
    public class UserService : IUserService
    {
        public bool AddUser(SaveUserDto dto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
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
        }

        public UserDto GetUser(int Id)
        {
            using (UnitOfWork unit = new UnitOfWork())
                return unit.Repostiroy<UserDto>().Get(Id);
        }
    }
}