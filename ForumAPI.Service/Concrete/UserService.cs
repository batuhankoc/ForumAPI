using AutoMapper;
using ForumAPI.Contract.DeleteContract;
using ForumAPI.Contract.LoginContract;
using ForumAPI.Contract.UserContract;
using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using ForumAPI.Service.Abstract;
using ForumAPI.Service.Exceptions;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddUserAsync(AddUserContract user)
        {
            var addUser = _mapper.Map<User>(user);

            var emailValid = await _userRepository.GetUserByEmail(user.Email);

            if (emailValid != null)
            {
                throw new ClientSideException("This email has found");
            }

            await _userRepository.AddAsync(addUser);
        }

        public async Task<User> LoginAsync(UserLoginContract login)
        {
            var loginmapp = _mapper.Map<User>(login);
            var isLoginValid = await _userRepository.Login(login.Email, login.Password);
            if (isLoginValid == null)
            {
                throw new ClientSideException("Wrong password or email");
            }
            return loginmapp;
        }

        public async Task DeleteUser(DeleteContract deleteContract)
        {
            var dbUser = await _userRepository.GetByIdAsync(deleteContract.Id);
            if(dbUser?.IsDeleted == true) { throw new ClientSideException("Kullanıcı bulunamadı"); };
            await _userRepository.RemoveAsync(dbUser);
        }
    }

}

