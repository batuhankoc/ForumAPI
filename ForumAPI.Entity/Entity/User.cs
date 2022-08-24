using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ForumAPI.Data.Entity
{
    [JsonObject(IsReference = true)]

    public partial class User : IEntity, ISoftDelete, IHasUpdatedAt
    {
        public User()
        {
            Answers = new HashSet<Answer>();
            Favorites = new HashSet<Favorite>();
            QuestionViews = new HashSet<QuestionView>();
            Questions = new HashSet<Question>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
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
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
