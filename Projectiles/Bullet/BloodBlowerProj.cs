using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using Microsoft.CodeAnalysis;

namespace RealmOne.Projectiles.Bullet
{
    public class BloodBlowerProj : ModProjectile
    {

         public override void SetStaticDefaults()
           {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;

        }
    
        
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.scale = 1f;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

        }
            public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.2f);
                Color color = new Color(244, 204, 39) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
        /*    public bool IsStickingToTarget
            {
                get => Projectile.ai[0] == 1f;
                set => Projectile.ai[0] = value ? 1f : 0f;
            }

            // Index of the current target
            public int TargetWhoAmI
            {
                get => (int)Projectile.ai[1];
                set => Projectile.ai[1] = value;
            }

            public int GravityDelayTimer
            {
                get => (int)Projectile.ai[2];
                set => Projectile.ai[2] = value;
            }

            public float StickTimer
            {
                get => Projectile.localAI[0];
                set => Projectile.localAI[0] = value;
            }



            private const int GravityDelay = 45;

            public override void AI()
            {
                // Run either the Sticky AI or Normal AI
                // Separating into different methods helps keeps your AI clean
                if (IsStickingToTarget)
                {
                    StickyAI();
                }
                else
                {
                    NormalAI();
                }
            }

            private void NormalAI()
            {
                GravityDelayTimer++; // doesn't make sense.

                // For a little while, the javelin will travel with the same speed, but after this, the javelin drops velocity very quickly.
                if (GravityDelayTimer >= GravityDelay)
                {
                    GravityDelayTimer = GravityDelay;

                    // wind resistance
                    Projectile.velocity.X *= 0.98f;
                    // gravity
                    Projectile.velocity.Y += 0.35f;
                }

                // Offset the rotation by 90 degrees because the sprite is oriented vertiacally.
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

                // Spawn some random dusts as the javelin travels
                if (Main.rand.NextBool(3))
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Blood, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                    dust.velocity += Projectile.velocity * 0.3f;
                    dust.velocity *= 0.2f;
                }

            }

            private const int StickTime = 60 * 15; // 15 seconds
            private void StickyAI()
            {
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                StickTimer += 1f;

                // Every 30 ticks, the javelin will perform a hit effect
                bool hitEffect = StickTimer % 30f == 0f;
                int npcTarget = TargetWhoAmI;
                if (StickTimer >= StickTime || npcTarget < 0 || npcTarget >= 200)
                { // If the index is past its limits, kill it
                    Projectile.Kill();
                }
                else if (Main.npc[npcTarget].active && !Main.npc[npcTarget].dontTakeDamage)
                {
                    // If the target is active and can take damage
                    // Set the projectile's position relative to the target's center
                    Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity * 2f;
                    Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
                    if (hitEffect)
                    {
                        // Perform a hit effect here, causing the npc to react as if hit.
                        // Note that this does NOT damage the NPC, the damage is done through the debuff.
                        Main.npc[npcTarget].HitEffect(0, 1.0);
                    }
                }
                else
                { // Otherwise, kill the projectile
                    Projectile.Kill();
                }
            }

            public override void Kill(int timeLeft)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound
                Vector2 usePos = Projectile.position; // Position to use for dusts

                // Offset the rotation by 90 degrees because the sprite is oriented vertiacally.
                Vector2 rotationVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
                usePos += rotationVector * 16f;

                // Spawn some dusts upon javelin death
                for (int i = 0; i < 20; i++)
                {
                    // Create a new dust
                    Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
                    dust.position = (dust.position + Projectile.Center) / 2f;
                    dust.velocity += rotationVector * 2f;
                    dust.velocity *= 0.5f;
                    dust.noGravity = true;
                    usePos -= rotationVector * 8f;
                }

                // Make sure to only spawn items if you are the projectile owner.
                // This is an important check as Kill() is called on clients, and you only want the item to drop once

            }

            private const int MaxStickingJavelin = 6; // This is the max amount of javelins able to be attached to a single NPC
            private readonly Point[] stickingJavelins = new Point[MaxStickingJavelin]; // The point array holding for sticking javelins

            public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
            {
                IsStickingToTarget = true; // we are sticking to a target
                TargetWhoAmI = target.whoAmI; // Set the target whoAmI
                Projectile.velocity = (target.Center - Projectile.Center) *
                    0.75f; // Change velocity based on delta center of targets (difference between entity centers)
                Projectile.netUpdate = true; // netUpdate this javelin
                Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore

                // ExampleJavelinBuff handles the damage over time (DoT)

                // KillOldestJavelin will kill the oldest projectile stuck to the specified npc.
                // It only works if ai[0] is 1 when sticking and ai[1] is the target npc index, which is what IsStickingToTarget and TargetWhoAmI correspond to.
                Projectile.KillOldestJavelin(Projectile.whoAmI, Type, target.whoAmI, stickingJavelins);
            }

            public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
            {
                // For going through platforms and such, javelins use a tad smaller size
                width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
                return true;
            }

            public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
            {
                // By shrinking target hitboxes by a small amount, this projectile only hits if it more directly hits the target.
                // This helps the javelin stick in a visually appealing place within the target sprite.
                if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
                {
                    targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
                }
                // Return if the hitboxes intersects, which means the javelin collides or not
                return projHitbox.Intersects(targetHitbox);
            }



            // Change this number if you want to alter how the alpha changes
        */

    }
}
