using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using Terraria.Localization;
using Terraria.Audio;

namespace RealmOne.Items.Weapons.Ranged
{
    public class JungleTechShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Tech Shotgun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'A bio-infused boomstick with increased shoot speed'"
            + "\n'Snazzy!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.shootSpeed = 40f;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item36;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 180);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(30);
            position += Vector2.Normalize(velocity) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JungleSpores, 8);
            recipe.AddIngredient(Mod, "LeadGunBarrel", 1);
            recipe.AddIngredient(ItemID.RichMahogany, 10);
            recipe.AddIngredient(Mod, "GizmoScrap", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();


            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.JungleSpores, 8);
            recipe1.AddIngredient(Mod, "IronGunBarrel", 1);
            recipe1.AddIngredient(ItemID.RichMahogany, 10);
            recipe1.AddIngredient(Mod, "GizmoScrap", 8);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }




        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(5, 0);
            return offset;
        }

    }
}