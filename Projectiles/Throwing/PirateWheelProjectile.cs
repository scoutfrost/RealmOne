using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Throwing
{
    public class PirateWheelProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bro thinks his bro :skull:");

        }

        public override void SetDefaults()
        {
            Projectile.width = 84;
            Projectile.height = 80;

            Projectile.aiStyle = ProjAIStyleID.Typhoon;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;

            Projectile.tileCollide = false;
            Projectile.timeLeft = 800;
            Projectile.penetrate = -2;
            AIType = ProjectileID.Typhoon;
            Projectile.scale = 1.5f;

        }

        public override void Kill(int timeleft)
        {

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item127, Projectile.position);
            for (int i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BorealWood, 0f, 0f, 0, default, 1.5f);

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            target.AddBuff(BuffID.ShadowFlame, 280);
            SoundEngine.PlaySound(SoundID.Item126, Projectile.position);
            for (int i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water_Corruption, 0f, 0f, 0, default, 2.5f);
        }

        public static void DrawGlowmask(PlayerDrawSet info)
        {
            Player Projectile = info.drawPlayer;

            if (Projectile.itemAnimation != 0)
            {
                Texture2D tex = Request<Texture2D>("RealmOne/Assets/Hilt").Value;
                Texture2D tex2 = Request<Texture2D>("RealmOne/Assets/hglow").Value;
                var frame = new Rectangle(0, 0, 50, 50);
                Color color = Lighting.GetColor((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16);
                var origin = new Vector2(Projectile.direction == 1 ? 0 : frame.Width, frame.Height);

                info.DrawDataCache.Add(new DrawData(tex, info.ItemLocation - Main.screenPosition, frame, color, Projectile.itemRotation, origin, Projectile.HeldItem.scale, info.itemEffect, 1));
                info.DrawDataCache.Add(new DrawData(tex2, info.ItemLocation - Main.screenPosition, frame, Color.LimeGreen, Projectile.itemRotation, origin, Projectile.HeldItem.scale, info.itemEffect, 1));
            }
        }
    }
}