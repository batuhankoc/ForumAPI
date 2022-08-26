using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.QuestionContract
{
    public class PaginationResponseContract<T> where T : class 
    {
        public List<T> Data { get; set; }
        public PaginationContract Pagination { get; set; }
    }
}
