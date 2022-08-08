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
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly DbSet<Question> _dbSet;
        public QuestionRepository(DataContext context) : base(context)
        {
            _dbSet = context.Set<Question>();
        }

    }
}
