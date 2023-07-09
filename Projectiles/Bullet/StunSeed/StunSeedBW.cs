﻿using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet.StunSeed
{
    public class StunSeedBW : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.damage = 33;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
        }
        int bounce = 0;
        int maxBounces = 5;

        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;

            Projectile.velocity.Y += 0.5f;
            Projectile.velocity.X *= 1f;
            Projectile.aiStyle = 0;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounce++;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X / 2;
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y / 2;
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            Projectile.aiStyle = 1;

            if (bounce >= maxBounces) return true;
            else return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 7; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.WoodFurniture, speed * 8, Scale: 1f);
                dust1.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}