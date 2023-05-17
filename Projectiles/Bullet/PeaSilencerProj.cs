using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{

	public class PeaSilencerProj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Agent Shot");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 16;

			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 0.1f;
			Projectile.tileCollide = true;
			Projectile.timeLeft = 600;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 1;
			AIType = 0;
			Projectile.CritChance = 100;
		}
		public override void AI()
		{
			Projectile.aiStyle = 0;

			Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.KryptonMoss, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.4f);
		}
		public override void Kill(int timeleft)
		{
			for (int i = 0; i < 6; i++)
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenBlood, 0f, 0f, 0, default, 1f);

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Tink, Projectile.position);

		}
	}
}

