using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class Wheel : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosive Projectile");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = ProjAIStyleID.GroundProjectile;
            Projectile.tileCollide = true;
            AIType = ProjectileID.Glowstick;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Explode();
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Explode();
        }


        private void Explode()
        {
            SoundEngine.PlaySound(SoundID.Tink);
            for (int i = 0; i < 10; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.MolotovFire3, Projectile.damage, Projectile.knockBack, Main.myPlayer);
            Projectile.Kill();
        }

    }


}
