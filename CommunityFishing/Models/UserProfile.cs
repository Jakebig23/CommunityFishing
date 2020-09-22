using System;
using System.Collections.Generic;

namespace CommunityFishing.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Userfish = new HashSet<Userfish>();
        }

        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearsFishing { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserAccountId { get; set; }

        public virtual ICollection<Userfish> Userfish { get; set; }
    }
}
