using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class Lightbulb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0,0,0,80);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 2);

            recipe.AddIngredient(ModContent.ItemType<Lightbulb>(), 2);
            recipe.Register();
        }
    }
}