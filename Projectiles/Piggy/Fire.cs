﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace RealmOne.Projectiles.Piggy
{
    public class Fire : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shockwave");
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 900;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
            Main.dust[dust].scale = 1.5f;
            Main.dust[dust].noGravity = true;

        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        

    }
}