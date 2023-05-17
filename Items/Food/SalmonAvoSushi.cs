using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealmOne.Buffs;
using Terraria.GameContent.Creative;

namespace RealmOne.Items.Food
{
    public class SalmonAvoSushi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Salmon Avo Sushi Roll"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'A tasty blend of the most carefully caught salmon in the ocean and fresh creamy slices of avocado'"
            + "\nGives Minor Improvements to all stats"
            + "\nIncreases Charm Duration by 8+ seconds"
            + "\nHeals 70 life");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;


            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(50, 200,50),
                new Color(20, 230, 180),
                new Color(20, 140, 20)
            };
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.AppleJuice);
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item2;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.healLife = 70;
        }





        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "FreshSeaweed", 2);
            recipe.AddIngredient(ItemID.Salmon, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}