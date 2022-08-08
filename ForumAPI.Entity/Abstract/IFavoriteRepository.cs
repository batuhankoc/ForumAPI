using ForumAPI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Abstract
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<bool> CheckFavorite( int questionId , int userId);
    }
}
