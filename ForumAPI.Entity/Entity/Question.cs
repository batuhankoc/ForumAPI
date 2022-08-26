using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ForumAPI.Data.Entity
{
    [JsonObject(IsReference = true)]

    public partial class Question : IEntity, ISoftDelete, IHasUpdatedAt
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Favorites = new HashSet<Favorite>();
            QuestionViews = new HashSet<QuestionView>();
            Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? UpdatedTime { get; set; }
        public string Content { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<QuestionView> QuestionViews { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
