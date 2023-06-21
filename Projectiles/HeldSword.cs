using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace RealmOne.Projectiles
{
    internal abstract class HeldSword : ModProjectile
    {
        protected Player Owner => Main.player[Projectile.owner];

        protected float StartPointUpSwingAngle;
        protected float StartPointAngle;
        protected float SwingSpeed;
        protected bool HasUpswing;
        protected bool UpSwing;
        protected float CollisionLength;
        protected float CollisionWidth;
        protected float EndPointAngle;

        private float startPointRot;
        private float startPointUpSwingRot;
        private float rotationOffset;
        private float endPointRot;
        public abstract void AbstractAI();
        public abstract void AbstractSetDefaults();
        public virtual Vector2 HoldOutOffset() => Vector2.Zero;
        public virtual float Scale() => 1;
        public override void OnSpawn(IEntitySource source)
        {
            rotationOffset = Owner.direction == 1 ? 0 : MathHelper.Pi;

            startPointRot = Owner.DirectionTo(Main.MouseWorld).RotatedBy(StartPointAngle).ToRotation() + rotationOffset;
            startPointUpSwingRot = Owner.DirectionTo(Main.MouseWorld).RotatedBy(StartPointUpSwingAngle).ToRotation() + rotationOffset;
            endPointRot = Owner.DirectionTo(Main.MouseWorld).RotatedBy(EndPointAngle).ToRotation() + rotationOffset;

            if (UpSwing)
                Projectile.rotation = startPointUpSwingRot;
            else
                Projectile.rotation = startPointRot;
        }
        public override void AI()
        {
            Vector2 ownerMountedCenter = Owner.RotatedRelativePoint(Owner.MountedCenter, true);
            Projectile.direction = Owner.direction;
            Owner.heldProj = Projectile.whoAmI;
            Owner.itemTime = 2;
            Owner.itemAnimation = 2;
            Projectile.Center = ownerMountedCenter + HoldOutOffset() + (Owner.direction == 1 ? Projectile.rotation.ToRotationVector2() : (Projectile.rotation + MathHelper.Pi).ToRotationVector2());
            Owner.ChangeDir(Projectile.direction);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.scale = Scale();

            if ((Owner.direction == 1 && !UpSwing) || Owner.direction == -1 && UpSwing)
                Projectile.rotation += SwingSpeed;
            if ((Owner.direction == -1 && !UpSwing) || Owner.direction == 1 && UpSwing)
                Projectile.rotation -= SwingSpeed;

            if (Owner.direction == 1)
            {
                if (Projectile.rotation >= endPointRot)
                    Projectile.Kill();
            }
            else
            {
                if (Projectile.rotation <= endPointRot)
                    Projectile.Kill();
            }

            if (Owner.direction == 1)
                Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.Pi);
            else
                Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.Pi - MathHelper.PiOver4);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float rotationFactor = Owner.direction == 1 ? Projectile.rotation : Projectile.rotation + MathHelper.Pi;
            float collisionPoint = 0f;

            Vector2 hitLineEnd = Projectile.Center + rotationFactor.ToRotationVector2() * CollisionLength;

            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, hitLineEnd, CollisionWidth * Projectile.scale, ref collisionPoint))
            {
                return true;
            }
            return false;
        }
    }
}