using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using RealmOne.Items.Placeables;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    public class TatteredBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LeadBow);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-1, 0);
            return offset;
        }
    }
}
