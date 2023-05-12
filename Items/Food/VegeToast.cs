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
    public class VegeToast : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vegemite Toast"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'How do people not like VEGEMITE!!!'");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(50, 200,50),
                new Color(20, 230, 180),
                new Color(20, 140, 20)
            };

        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CookedFish);
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = 2;
            Item.consumable = true;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Toast");

        }



        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "JamToast", "'Tastes like Australia!'")
            {
                OverrideColor = new Color(220, 197, 73)

            };
            tooltips.Add(line);
        }
        public override void AddRecipes()
        {
            CreateRecipe(3)
            .AddIngredient(Mod, "BreadLoaf", 1)
            .AddTile(TileID.Furnaces)
            .Register();


        }

    }
}