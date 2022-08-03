using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ForumAPI.Data.Entity
{
    public partial class User
    {
        
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public string? Image { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string Location { get; set; } = null!;

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<QuestionView> QuestionViews { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
