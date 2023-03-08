using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System;
using Terraria.GameContent;

namespace RealmOne.Projectiles.Bullet
{

    public class TommyGunBarrelFlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Muzzle Flash");
        }
        private Vector2 flashoffset = Vector2.Zero;


        private Player Owner => Main.player[Projectile.owner];

        private bool FullyUsed = false;


        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.damage = 0;
            Projectile.height = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 5;
            Projectile.friendly = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.4f);
            Projectile.Center = Owner.Center + flashoffset;
            Projectile.rotation = Projectile.ai[0];
            if (!FullyUsed)
            {
                FullyUsed = true;
                flashoffset = Projectile.Center - Owner.Center;
            }



            float offset = 10;

            Projectile.spriteDirection = Projectile.direction;


            Projectile.Center = player.Center + Projectile.rotation.ToRotationVector2() * offset;

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D mainTex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(mainTex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(8, mainTex.Height / 2), Projectile.scale, SpriteEffects.None, 0f);

            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Color glowColor = Color.Orange;
            glowColor.A = 0;
            Main.spriteBatch.Draw(glowTex, Projectile.Center - Main.screenPosition, null, glowColor, Projectile.rotation, new Vector2(8, glowTex.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }


    }

}





