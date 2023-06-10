using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class JamToast : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Strawberry Jam Toast"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'What I used to wake up to in the morning as a kid'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
                new Color(196, 135, 4),
                new Color(222, 79, 216 ),
            };
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.WellFed3, 57600);

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Toast");

        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "JamToast", "'Excellent with home style butter!'")
            {
                OverrideColor = new Color(211, 197, 73)

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