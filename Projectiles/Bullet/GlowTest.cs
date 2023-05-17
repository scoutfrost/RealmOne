using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{

	public class GlowTest : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pristine Beam Blade");
			Main.projFrames[Projectile.type] = 5;

			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 160;
			Projectile.height = 46;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 300;
			Projectile.light = 2;
			Projectile.penetrate = 2;
			Projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			float maxDetectRadius = 200;
			float projSpeed = 14;

			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			if (closestNPC == null)
				return;

			Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
			Projectile.rotation = Projectile.velocity.ToRotation();

			Projectile.aiStyle = 0;
			Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
			Projectile.rotation = Projectile.velocity.ToRotation();
			Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Enchanted_Gold, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1f);   //spawns dust behind it, this is a spectral light blue dust

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			SoundEngine.PlaySound(rorAudio.SFX_OldGoldBeam);

		}
		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawPosition = Projectile.Center;
			Color color = Color.Cyan;
			_ = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
			return true;
		}

		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];

				if (target.CanBeChasedBy())
				{

					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}
	}
}