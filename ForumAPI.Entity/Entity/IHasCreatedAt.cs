using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Data.Entity
{
    public interface IHasUpdatedAt
    {
        DateTime? UpdatedTime { get; set; }
    }
}
