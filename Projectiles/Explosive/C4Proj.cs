using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using RealmOne.Common.DamageClasses;

namespace RealmOne.Projectiles.Explosive
{
    public class C4Proj : ModProjectile
    {
        public bool Exploded { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = !value ? 0 : 1; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("C4");

        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = ProjAIStyleID.StickProjectile;
            Projectile.DamageType = ModContent.GetInstance<DemolitionClass>();


            Projectile.ignoreWater = true;

            Projectile.tileCollide = true;
            Projectile.timeLeft = 1200;
            Projectile.penetrate = 1;
            DrawOffsetX = 5;
            DrawOriginOffsetY = 5;


            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.hostile = false;
        }

        public override void Kill(int timeleft)
        {

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
            for (var i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 1f);



            for (float i = 0; i <= 3f; i += Main.rand.NextFloat(0.5f, 2))
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, i.ToRotationVector2() * Main.rand.NextFloat(), ProjectileID.ClusterFragmentsI, (int)(Projectile.damage * 0.5f), Projectile.knockBack, Projectile.owner);

            for (int i = 0; i < 8; i++)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Scale: Main.rand.NextFloat(1f, 1.5f)).noGravity = true;

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
            target.AddBuff(BuffID.OnFire, 180);
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
            for (var i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 2f);
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



            if (Projectile.timeLeft < 1200 && !Exploded)
            {
                const int ExplosionSize = 400;

                Projectile.position -= new Vector2(ExplosionSize / 2f) - Projectile.Size / 2f;
                Projectile.width = Projectile.height = ExplosionSize;
                Projectile.hostile = true;
                Projectile.friendly = true;
                Projectile.hide = true;
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
                Exploded = true;

            }
            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy(Projectile.rotation, default) * 1.1f;


        }
    }
}





