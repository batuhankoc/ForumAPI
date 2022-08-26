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
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly DbSet<Favorite> _favorite;

        public FavoriteRepository(DataContext context) : base(context)
        {
            _favorite = context.Set<Favorite>();
        }

        public async Task<bool> CheckFavorite(int questionId, int userId)
        {
            var isFavorited = await _favorite.AnyAsync(x => x.UserId == userId && x.QuestionId == questionId);
            return isFavorited;

        }

       
    }
}
