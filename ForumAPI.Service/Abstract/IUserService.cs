using ForumAPI.Contract.DeleteContract;
using ForumAPI.Contract.LoginContract;
using ForumAPI.Contract.UserContract;
using ForumAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Abstract
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserContract user);
        Task<User> LoginAsync(UserLoginContract login);
        Task DeleteUser(DeleteContract deleteContract);
    }
}
