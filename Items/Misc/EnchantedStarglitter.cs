/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class EnchantedStarglitter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Gem");
            Tooltip.SetDefault("'Paraphernalia of a shooting star!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;


        }

        public override void SetDefaults()
        {
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.width = 20;    
            Item.height = 20;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Lighting.AddLight(Item.position, r: 0.08f, g: 0.1f, b: 0.15f);
            
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(5)
            .AddIngredient(ItemID.FallenStar, 3)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}*/