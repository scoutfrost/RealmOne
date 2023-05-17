using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using RealmOne.Rarities;

namespace RealmOne.Items.Misc
{
    public class WoodenGunBarrel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden Gun Barrel"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Crafts wooden based guns with a single barrel"
                + "\n'The barrel is polished and thick, incapable for it to be set on fire'"
                + "\n'Common use of it is typically used for weaker but reliable guns'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;


        }


        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 99;
            Item.value = Item.buyPrice(gold: 1, silver: 5);


        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddRecipeGroup("Wood", 25)
            .AddTile(TileID.WorkBenches)
            .Register();

        }

    }
}