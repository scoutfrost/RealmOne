using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Common.DamageClasses;
using RealmOne.Projectiles.Magic;

namespace RealmOne.Projectiles.Explosive
{
    public class WormBall : ModProjectile
    {

        public bool Exploded { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = !value ? 0 : 1; }


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butthole Worms");

            Main.projFrames[Projectile.type] = 2;


        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.hostile = false;
            Projectile.timeLeft = 260;
            Projectile.penetrate = 2;
            Projectile.DamageType = ModContent.GetInstance<DemolitionClass>();


            Projectile.ownerHitCheck = true;
        }

        public override void AI()
        {

            Projectile.rotation += 0.05f * Projectile.velocity.X;
            Projectile.velocity.Y += 0.2f;

            if (++Projectile.frameCounter >= 8f)//the amount of ticks the game spends on each frame
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            if (Projectile.timeLeft < 4 && !Exploded)
            {
                const int ExplosionSize = 240;

                Projectile.position -= new Vector2(ExplosionSize / 2f) - Projectile.Size / 2f;
                Projectile.width = Projectile.height = ExplosionSize;
                Projectile.hostile = true;
                Projectile.friendly = true;
                Projectile.hide = true;
                Exploded = true;
            }
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Worm, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Mud, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy(Projectile.rotation, default) * 1.1f;


        }

        public override bool OnTileCollide(Vector2 oldVelocity)

        {
            Projectile.alpha = 255;
            // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
            Projectile.position.X = Projectile.position.X + Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
            Projectile.width = 150;
            Projectile.height = 150;
            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            Projectile.damage = 19;
            Projectile.knockBack = 2f;
            Projectile.penetrate = 1;
            Projectile.ownerHitCheck = false;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<SquirmStaffProjectile>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);


            return true;

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OgreSpit, 140);

        }

        public override bool? CanHitNPC(NPC target) => !target.friendly;
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<SquirmStaffProjectile>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, Projectile.position);

            Projectile.tileCollide = true;
            // Set to transparant. This projectile technically lives as  transparant for about 3 frames
            Projectile.alpha = 255;
            // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
            Projectile.position.X = Projectile.position.X + Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            Projectile.damage = 19;
            Projectile.knockBack = 2f;
            Projectile.penetrate = 1;
            Projectile.ownerHitCheck = false;
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PinkSlime, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Mud, 0f, 0f, 150, default, 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Worm, 0f, 0f, 100, default, 1f);
                Main.dust[dustIndex].velocity *= 3f;
            }
        }
    }
}





