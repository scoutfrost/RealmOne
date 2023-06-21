using Microsoft.Xna.Framework;
using RealmOne.Rarities;
using RealmOne.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
namespace RealmOne.Items.Misc
{
    public class BreadLoaf : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bread Loaf"); 
            Tooltip.SetDefault($"[c/BBA95E:'Freshly baked from the rustic and antique stone baker ovens']");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;

        }

        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = 20000;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 2);

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "BreadLoaf", "'Strawberry Jam is my personal favourite.'")
            {
                OverrideColor = new Color(226, 34, 90)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BreadLoaf", "'Vegemite is also good. An aussie classic :)'")
            {
                OverrideColor = new Color(222, 150, 0)

            };
            tooltips.Add(line);

        }
        public override void AddRecipes()
        {
            CreateRecipe(5)
            .AddIngredient(Mod, "Wheat", 15)
                            .AddTile<StoneOvenTilee>()

            .Register();

        }
    }
}