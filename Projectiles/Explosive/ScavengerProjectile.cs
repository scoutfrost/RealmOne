using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RealmOne.Projectiles.Explosive
{
    public class ScavengerProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rat Razor");

        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = 14;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 540;
            Projectile.penetrate = -2;
            AIType = ProjectileID.SpikyBall;
            DrawOffsetX = 5;
            DrawOriginOffsetY = 5;
        }

        public override void Kill(int timeleft)
        {

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
            for (var i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 0, default, 1.5f);



            for (float i = 0; i <= 3f; i += Main.rand.NextFloat(0.5f, 2))
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, i.ToRotationVector2() * Main.rand.NextFloat(), ProjectileID.Electrosphere, (int)(Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner);

            for (int i = 0; i < 8; i++)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Scale: Main.rand.NextFloat(1f, 1.5f)).noGravity = true;

            // These gores work by simply existing as a texture inside any folder which path contains "Gores/"
            int RumGore1 = Mod.Find<ModGore>("SawGore1").Type;
            int RumGore2 = Mod.Find<ModGore>("SawGore2").Type;
            int RumGore3 = Mod.Find<ModGore>("SawGore3").Type;

            var entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 3; i++)
            {
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore1);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore2);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore3);

            }

        }




        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            target.immune[Projectile.owner] = 20;
            Projectile.Kill();
            target.AddBuff(BuffID.Electrified, 180);
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
            for (var i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 0, default, 2f);
        }



        public override void AI()
        {
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Scale: Main.rand.NextFloat(0.5f, 1f)).noGravity = true;

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 7200f)
                Projectile.Kill();
            Lighting.AddLight((int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f), 0.196f, 0.870588235f, 0.964705882f);
            Projectile.localAI[0] += 1f;
            if (Main.mouseRight && Main.myPlayer == Projectile.owner)
                Projectile.ai[1] = 7201;
            if (Projectile.localAI[0] >= 10f)
            {
                Projectile.localAI[0] = 0f;
                int num416 = 0;
                int num419 = Projectile.type;
                for (int num420 = 0; num420 < 1000; num420++)
                {
                    if (Main.projectile[num420].active && Main.projectile[num420].owner == Projectile.owner && Main.projectile[num420].type == num419)
                        num416++;

                    if (num416 > 5)
                    {
                        Projectile.netUpdate = true;
                        Projectile.Kill();
                        return;
                    }
                }
            }
        }
    }

}