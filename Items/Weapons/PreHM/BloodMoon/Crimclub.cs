using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using RealmOne.Projectiles;

namespace RealmOne.Items.Weapons.PreHM.BloodMoon
{
    public class Crimclub : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 66;
            Item.crit = 45;
            Item.damage = 34;
            Item.knockBack = 10f;

            Item.useAnimation = 46;
            Item.useTime = 46;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.noMelee = true;


            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<CrimclubSwing>();
        }
        int swingDirection = 1;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            swingDirection = -swingDirection;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0, swingDirection);
            return false;
        }
    }

    public class CrimclubSwing : HeldSword
    {
        public override string Texture => "RealmOne/Items/Weapons/PreHM/BloodMoon/Crimclub";
      
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }
        public override void AbstractAI()
        {
            Player player = Main.player[Projectile.owner];
           
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hitinfo, int damage)
        {
            for (int i = 0; i < 2; i++)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, new Vector2(Main.rand.NextFloat(-2, 2), -3), ProjectileID.BloodNautilusShot, (int)(Projectile.damage * 0.3f), 0, 0);
            }
        }

        public override void AbstractSetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 66;
            SwingSpeed = 30;
        }


    }
}
