using System;
using System.Collections.Generic;

namespace CommunityFishing.Models
{
    public partial class Userfish
    {
        public Userfish()
        {
            PublicBlog = new HashSet<PublicBlog>();
        }

        public int UserFishId { get; set; }
        public string UserAccountId { get; set; }
        public string FishName { get; set; }
        public string FishLength { get; set; }
        public string UserFishPhotoPath { get; set; }
        public DateTime CatchDate { get; set; }

        public virtual FishInfo FishNameNavigation { get; set; }
        public virtual UserProfile UserAccount { get; set; }
        public virtual ICollection<PublicBlog> PublicBlog { get; set; }
    }
}
