using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using System;
using RealmOne.Projectiles.Magic;
using RealmOne.Projectiles.Other;
using System.Reflection.Metadata;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace RealmOne.Projectiles.HeldProj
{

    public class SandTwisterProj : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Yoyo");
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 4f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300f;
            // YoyosTopSpeed is top speed of the yoyo projectile. 
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13f;
            Projectile.CloneDefaults(ProjectileID.TheEyeOfCthulhu);

        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Valor);
            Projectile.damage = 16;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Valor;

        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 140)
            {
                float angle = Main.rand.NextFloat(MathHelper.PiOver4, -Microsoft.Xna.Framework.MathHelper.Pi - MathHelper.PiOver2);
                Vector2 PositionArea = Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 35f;

                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - PositionArea) * 1f;


                Projectile.frameCounter = 0;
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, velocity.X, velocity.Y, ModContent.ProjectileType<MiniSand>(), Projectile.damage, Projectile.owner, 0, 0f);
                
            }
        }
        public override void PostAI()
        {
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado);
                dust.noGravity = true;
                dust.scale = 1.6f;

            }
        }
    }
}