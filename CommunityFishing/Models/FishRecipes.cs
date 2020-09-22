using System;
using System.Collections.Generic;

namespace CommunityFishing.Models
{
    public partial class FishRecipes
    {
        public int FishRecipeId { get; set; }
        public string FishName { get; set; }
        public string RecipePhotoPath { get; set; }

        public virtual FishInfo FishNameNavigation { get; set; }
    }
}
