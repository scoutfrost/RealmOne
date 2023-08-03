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
    public class TatteredSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LeadBroadsword);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 12)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
