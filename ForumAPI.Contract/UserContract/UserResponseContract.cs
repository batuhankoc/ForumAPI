using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumAPI.Contract.UserContract
{
    public class UserResponseContract
    {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string? Image { get; set; }
            public int Id { get; set; }

    }
}
