using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.HeldProj
{
    public class WaterQuarryHeld : ModProjectile
    {
        private float AimResponsiveness = 1f;
        private bool timerUp = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;//number of frames the animation has
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 3;
            Projectile.ownerHitCheck = true;//so it cant attack through walls
            Projectile.scale = 1f;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 70f)
            {
                Projectile.ai[0] = -1f;
                timerUp = true;
                Projectile.netUpdate = true;
            }

            if (timerUp == true)
                Projectile.ai[0] -= 1;

            if (player.channel == false)
            {
                Projectile.Kill();
            }

            if (Projectile.ai[0] == 60)
                ShootBullets();
            if (Projectile.ai[0] == 68)
                ShootBullets();

            Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);

            bool stillInUse = player.channel && !player.noItems && !player.CCed;
            if (Projectile.owner == Main.myPlayer)
            {
                UpdatePlayerVisuals(player, rrp);

                UpdateAim(rrp, player.HeldItem.shootSpeed);

            }
            else if (!stillInUse)
                Projectile.timeLeft = 2;

        }
        private void UpdatePlayerVisuals(Player player, Vector2 playerhandpos)
        {
            Projectile.Center = playerhandpos;
            Projectile.spriteDirection = Projectile.direction;

            // Constantly resetting player.itemTime and player.itemAnimation prevents the player from switching items or doing anything else.
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            float piover2 = MathHelper.PiOver2;
            if (player.direction == 1)
            {
                piover2 -= MathHelper.Pi;
            }

            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + piover2);
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();

        }
        private void UpdateAim(Vector2 source, float speed)
        {
            Player player = Main.player[Projectile.owner];
            var aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
                aim = -Vector2.UnitY;
            Vector2 DirAndVel = new(Projectile.velocity.X * player.direction, Projectile.velocity.Y * player.direction);
            Projectile.rotation = DirAndVel.ToRotation();
            //lerp = to, from, speed to go
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, AimResponsiveness));
            aim *= speed;

            if (aim != Projectile.velocity)
                Projectile.netUpdate = true;
            Projectile.velocity = aim;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // get texture
            var texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            var sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;

            // (0,0) is upper-left corner

            //recoil
            float offsetX = -5f;

            origin.X = Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX;

            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            //return false to not draw original texture
            return false;
        }

        private void ShootBullets()
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(SoundID.Splash);
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(player.GetSource_ItemUse_WithPotentialAmmo(player.HeldItem, AmmoID.None), Projectile.Center, Projectile.velocity * 1f, ProjectileID.WaterBolt, Projectile.damage, Projectile.knockBack, player.whoAmI);            //screenshake

        }
    }
}