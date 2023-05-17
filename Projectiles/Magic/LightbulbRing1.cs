using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Diagnostics.Metrics;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

using Terraria.GameContent;
using System;
using System.Collections.Generic;

using Terraria.Audio;
using RealmOne.Common;
using RealmOne.Common.Systems;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Magic
{

    public class LightBulbRing1 : ModProjectile
    {
        private static Asset<Texture2D> LightbulbRing;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb Ring");

            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 260;
            Projectile.height = 260;

            Projectile.aiStyle = 9;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.light = 3.5f;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 100;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.penetrate = 5;
            Projectile.stepSpeed = 1f;
            Projectile.alpha = 255;
            Projectile.scale = 1f;


        }


        public override void AI() // this may add flamelash
        {
            //   AIType = ProjectileID.Flamelash;
            Lighting.AddLight(Projectile.position, 3f, 3f, 1f);
            Lighting.Brightness(2, 2);


            //  Projectile.localAI[0] += 1f;
        }

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            LightbulbRing = Request<Texture2D>("RealmOne/Assets/Effects/LightbulbShine");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            LightbulbRing = null;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(LightbulbRing.Value, Projectile.Center - Main.screenPosition,
                          LightbulbRing.Value.Bounds, Color.NavajoWhite, Projectile.rotation,
                          LightbulbRing.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            return true;
        }

    }
}



