using System;
using System.Collections.Generic;

namespace ForumAPI.Data.Entity
{
    public partial class Answer : IEntity , ISoftDelete , IHasUpdatedAt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Content { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool IsBestAnswer { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
