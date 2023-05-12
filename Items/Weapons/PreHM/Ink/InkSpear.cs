using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using RealmOne.Projectiles.HeldProj;
using RealmOne.Projectiles.Throwing;
using RealmOne.Rarities;

namespace RealmOne.Items.Weapons.PreHM.Ink
{
    public class InkSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Thrusts out an inky and sloppy spear"
                + "\nShoots out ink globs that stick to the floor");
            DisplayName.SetDefault("Ink Spear");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.Spears[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ModContent.RarityType<InkRarity>();
            Item.value = Item.sellPrice(silver: 95);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 29;
            Item.useTime = 29;
            Item.UseSound = SoundID.DD2_GoblinBomberThrow;
            Item.autoReuse = true;

            Item.damage = 18;
            Item.knockBack = 2f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<InkSpearProj>();
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Cactus, 20);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }

}