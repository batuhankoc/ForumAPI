using ForumAPI.Contract.QuestionContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Cache.Interfaces
{
    public interface IFavoriteCache
    {
        public Task<bool> CheckFav(int id, int userId);
        public Task RemoveFavoriteCache(int id, int userId);
    }
}
