using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Terraria.Audio;
using RealmOne.Common.Systems;
using RealmOne.Buffs;

namespace RealmOne.Items.Food
{
    public class ToastedNutBar: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toasted Nut Bar"); 
             Tooltip.SetDefault("'Extreme trace of nuts!'");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(195, 142, 77),
                new Color(222, 220, 96),
                new Color(157, 216, 66)
            };

        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = 2;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
            Item.buffType = ModContent.BuffType<ToastedNutBarBuff>();   
            Item.buffTime = 18000;
        }



       
        public override void AddRecipes()
        {
            CreateRecipe(2)
             .AddIngredient(ItemID.Acorn, 2)
                          .AddIngredient(ItemID.GrassSeeds, 2)

            .AddTile(TileID.Furnaces)
            .Register();


        }

    }
}