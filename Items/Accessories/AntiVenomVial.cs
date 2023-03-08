using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealmOne.Items.Accessories
{
    public class AntiVenomVial : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anti-Venom Vial"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Full immunity to Venom"
                + "\n'This anti-venom works for other critters too!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {


            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = 1;
            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Venom] = true;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Stinger, 3);
            recipe.AddIngredient(Mod,"GoopyGrass", 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
            
        }
    }
}