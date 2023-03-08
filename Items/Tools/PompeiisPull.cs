using IL.Terraria.GameContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace RealmOne.Items.Tools
{
    internal class PompeiisPull : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pompeiis Pull");

            Tooltip.SetDefault("Grapple to Italy's Doomsday, and back...");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {

            Item.shootSpeed = 28f; 
            Item.shoot = ModContent.ProjectileType<PompeiisPullProjectile>();
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.knockBack = 4;
            Item.value = 30000;
            Item.rare = 2;
            Item.damage = 28;
           
         
         
            Item.crit = 2;


        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "PompeiisPull", "'Gladius Grapplius!'")
            {
                OverrideColor = new Color(189, 220, 90)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "PompeiisPull", "What warriors used to get over grassy and steep cliffs")
            {
                OverrideColor = new Color(42, 138, 55)

            };
            tooltips.Add(line);


        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, (int)(GlowMaskID.GemBunny * 3f));

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.AmberBolt, velocity);
                dust.scale = 0.8f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = false;
                dust.alpha = 0;
            }
        }
        internal class PompeiisPullProjectile : ModProjectile
        {
            private static Asset<Texture2D> chainTexture;

            public override void Load()
            {
                chainTexture = ModContent.Request<Texture2D>("RealmOne/Items/Tools/PompeiisChain");
            }

            public override void Unload()
            { // This is called once on mod reload when this piece of content is being unloaded.
              // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
                chainTexture = null;
            }
            public override void Kill(int timeLeft)
            {
                for (var i = 0; i < 6; i++)
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GemEmerald, 0f, 0f, 0, default, 0.6f);

                Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item108);
            }
            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("${ProjectileName.GemHookRuby}");
            }

            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.GemHookRuby); // Copies the attributes of the Amethyst hook's projectile.
                Projectile.damage = 28;
                Projectile.CritChance = 5;
            }



            // Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook.
            public override bool? CanUseGrapple(Player player)
            {
                int hooksOut = 0;
                for (int l = 0; l < 1000; l++)
                {
                    if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
                    {
                        hooksOut++;
                    }
                }

                return hooksOut <= 1;
            }
            public override float GrappleRange()
            {
                return 410f;
            }

            public override void NumGrappleHooks(Player player, ref int numHooks)
            {
                numHooks = 1; // The amount of hooks that can be shot out
            }

            // default is 11, Lunar is 24
            public override void GrappleRetreatSpeed(Player player, ref float speed)
            {
                speed = 19f; // How fast the grapple returns to you after meeting its max shoot distance
            }

            public override void GrapplePullSpeed(Player player, ref float speed)
            {
                speed = 17; // How fast you get pulled to the grappling hook projectile's landing position
            }

            // Adjusts the position that the player will be pulled towards. This will make them hang 50 pixels away from the tile being grappled.
            public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
            {
                Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
                float hangDist = 40f;
                grappleX += dirToPlayer.X * hangDist;
                grappleY += dirToPlayer.Y * hangDist;
            }

            // Draws the grappling hook's chain.
            public override bool PreDrawExtras()
            {
                Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
                Vector2 center = Projectile.Center;
                Vector2 directionToPlayer = playerCenter - Projectile.Center;
                float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
                float distanceToPlayer = directionToPlayer.Length();

                while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
                {
                    directionToPlayer /= distanceToPlayer; // get unit vector
                    directionToPlayer *= chainTexture.Height(); // multiply by chain link length

                    center += directionToPlayer; // update draw position
                    directionToPlayer = playerCenter - center; // update distance
                    distanceToPlayer = directionToPlayer.Length();

                    Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                    // Draw chain
                    Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                        chainTexture.Value.Bounds, drawColor, chainRotation,
                        chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
                }
                // Stop vanilla from drawing the default chain.
                return false;
            }












        }



        }
}
