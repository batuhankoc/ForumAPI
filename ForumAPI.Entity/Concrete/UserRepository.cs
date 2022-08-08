using ForumAPI.Data.Abstract;
using ForumAPI.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(DataContext context) : base(context)
        {
            _dbSet = context.Set<User>();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(h => h.Email == email);
        }

        public async Task<User> Login(string email, string password)
        {
            return await _dbSet.FirstOrDefaultAsync(h => h.Email == email && h.Password == password);
        }

    }
}
