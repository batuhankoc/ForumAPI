using System;
using System.Collections.Generic;

namespace ForumAPI.Data.Entity
{
    public partial class Vote : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool? Voted { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
