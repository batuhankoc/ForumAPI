using ForumAPI.Contract.UserContract;
using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using ForumAPI.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(AddUserContract user)
        {
            var editUser = new User() {Email = "forum@gmail.com", Location = "Ankara", Password = "000", UserName = "ForumApi"}; 
            await _userRepository.AddAsync(editUser);
        }
    }
}
