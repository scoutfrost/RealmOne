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
    public class TatteredPick : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TinPickaxe);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
