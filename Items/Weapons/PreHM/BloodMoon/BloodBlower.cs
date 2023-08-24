using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Arrow;
using RealmOne.Projectiles.Bullet;
using RealmOne.Projectiles.Other;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.BloodMoon
{
    public class BloodBlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item64;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 36f;
            Item.shoot = ModContent.ProjectileType<BloodBlowerProj>();

        }
    
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            {
                Lighting.AddLight(player.position, 0.3f, 0.18f, 0.0f);

                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(7);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .5f; // Watch out for dividing by 0 if there is only 1 projectile.
                    
                    Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<BloodBlowerProj>(), damage, knockback, player.whoAmI);
                }
                return false;
            }
        }
  

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SandBlock, 15);
            recipe.AddIngredient(ItemID.SandstoneBrick, 15);
            recipe.AddIngredient(ItemID.MysticCoilSnake, 1);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-3, -3);
            return offset;
        }
    }
  
}
