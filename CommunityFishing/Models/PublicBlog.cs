using System;
using System.Collections.Generic;

namespace CommunityFishing.Models
{
    public partial class PublicBlog
    {
        public int BlogId { get; set; }
        public int? UserFishId { get; set; }
        public string BlogTitle { get; set; }
        public string Content { get; set; }
        public DateTime BlogDate { get; set; }

        public virtual Userfish UserFish { get; set; }
    }
}
