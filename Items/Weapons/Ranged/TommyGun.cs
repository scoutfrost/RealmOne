    using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using Terraria.Localization;
using Terraria.Audio;
using System;
using System.Collections.Generic;
using RealmOne.Rarities;
using RealmOne.Projectiles.Bullet;

namespace RealmOne.Items.Weapons.Ranged
{
    public class TommyGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tommy Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Fires .45 caliber medium bullets at a circulated rate"
            + "\n'The magazine is a circular drum that can hold up to 50-100 rounds."
            + $"\nUses Bullets [i:{ItemID.MusketBall}]");



            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
      
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.UseSound = SoundID.Item116;

            Item.rare = ModContent.RarityType<ModRarities>();
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 40f;
            Item.shoot = ProjectileID.Bullet;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/TommyGunShot");


        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {



            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 13f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            Gore.NewGore(source, player.Center + muzzleOffset * 1, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("TommyGunPellets").Type, 1f);

            for (int i = 0; i < 6; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Smoke, speed * 5, Scale: 2f);
                d.noGravity = true;
            }

            Projectile.NewProjectile(player.GetSource_ItemUse(Item), position + muzzleOffset, Vector2.Zero, ModContent.ProjectileType<TommyGunBarrelFlash>(), 0, 0, player.whoAmI);

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 10);
            recipe.AddIngredient(Mod, "FleshyCornea", 8);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(Mod, "IronGunBarrel", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddRecipeGroup("Wood", 10);
            recipe1.AddIngredient(Mod, "FleshyCornea", 8);
            recipe1.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe1.AddIngredient(Mod, "LeadGunBarrel", 1);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.38f;
        }

        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(-5, 0);
            return offset;
        }

    }
}