using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using RealmOne.Buffs.Alc;
using Terraria.ID;

namespace RealmOne.Items.Potions.Alcohol
{
    public class VintageBeer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Limetwist Rum"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("A slightly tangy but frothy rum"
            + "\nGives the Limetwist Rum Buff"
            + "\nIgnores 3 points of enemy defence, increasing endurance by 10% but decreased weapon speed by 17%"
                        + $"\nIngreients: [i:{ItemID.Lemon}], [i:{ItemID.Grapes}] [i:{ItemID.LimeKelp}]"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;

        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 5000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<BeerBuff>();
            Item.buffTime = 10000;
        }








        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddIngredient(ItemID.Lemon, 2);
            recipe.AddIngredient(ItemID.Grapes, 1);

            recipe.AddIngredient(ItemID.LimeKelp, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
