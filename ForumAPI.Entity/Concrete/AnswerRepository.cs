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
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        private readonly DbSet<Answer> _dbSet;

        public AnswerRepository(DataContext context) : base(context)
        {
            _dbSet = context.Set<Answer>();
        }

        
    }
}
