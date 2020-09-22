using System;
using System.Collections.Generic;

namespace CommunityFishing.Models
{
    public partial class FishInfo
    {
        public FishInfo()
        {
            FishRecipes = new HashSet<FishRecipes>();
            Userfish = new HashSet<Userfish>();
        }

        public int FishId { get; set; }
        public string FishName { get; set; }
        public string LegalLength { get; set; }
        public string LegalLimit { get; set; }
        public DateTime SeasonStart { get; set; }
        public DateTime SeasonEnd { get; set; }

        public virtual ICollection<FishRecipes> FishRecipes { get; set; }
        public virtual ICollection<Userfish> Userfish { get; set; }
    }
}
