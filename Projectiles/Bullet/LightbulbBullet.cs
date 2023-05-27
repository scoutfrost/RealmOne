using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Magic;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Bullet
{

    public class LightbulbBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb");
        }
      //  private Vector2 flashoffset = Vector2.Zero;

       // private Player Owner => Main.player[Projectile.owner];

      //  private bool FullyUsed = false;

        private static Asset<Texture2D> Spark;

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.aiStyle = 0;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.Left + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.8f);

            Player player = Main.player[Projectile.owner];

            Lighting.AddLight(Projectile.Center, r: 2f, g: 2.2f, 1f);
            //  Projectile.rotation = Projectile.ai[0];
            //   if (!FullyUsed)
            //  {
            //      FullyUsed = true;
            //   flashoffset = Projectile.Center - Owner.Center;
            //}
            //     Projectile.rotation = player.DirectionTo(Main.MouseWorld).ToRotation;
            //   Projectile.rotation = player.DirectionTo(Main.MouseWorld).ToRotation();

            //  Projectile.Center = Owner.Center + flashoffset;

            
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
        }
        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            Spark = Request<Texture2D>("RealmOne/Assets/Effects/lighty");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            Spark = null;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(Spark.Value, Projectile.Center - Main.screenPosition,
                          Spark.Value.Bounds, Color.NavajoWhite, Projectile.rotation,
                          Spark.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore1").Type, 1.5f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore2").Type, 1.5f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore3").Type, 1.5f);
            
        }


    }
}

