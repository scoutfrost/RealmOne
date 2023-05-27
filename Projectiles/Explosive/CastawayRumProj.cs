using Microsoft.Xna.Framework;
using RealmOne.Common.DamageClasses;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Explosive
{
	public class CastawayRumProj : ModProjectile
	{

		public bool Exploded { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = !value ? 0 : 1; }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Castaway Rum");

			Main.projFrames[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode

		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 60;
			Projectile.friendly = true;
			Projectile.extraUpdates = 1;
			Projectile.hostile = false;
			Projectile.timeLeft = 240;
			Projectile.penetrate = 1;
			Projectile.DamageType = ModContent.GetInstance<DemolitionClass>();
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = true;
			Projectile.scale = 0.6f;
			Projectile.aiStyle = ProjAIStyleID.MolotovCocktail;
		}

		public override void AI()
		{
			Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f);
			Projectile.aiStyle = ProjAIStyleID.MolotovCocktail;
			Projectile.rotation += 0.02f * Projectile.velocity.X;
			Projectile.velocity.Y += 0.14f;

			if (++Projectile.frameCounter >= 10f)//the amount of ticks the game spends on each frame
			{
				Projectile.frameCounter = 0;

				if (++Projectile.frame >= Main.projFrames[Projectile.type])
					Projectile.frame = 0;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.tileCollide = true;
			// Set to transparant. This projectile technically lives as  transparant for about 3 frames
			Projectile.alpha = 255;
			// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
			Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
			Projectile.damage = 19;
			Projectile.knockBack = 4f;
			Projectile.penetrate = 1;
			Projectile.ownerHitCheck = false;

			return true;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

		{
			target.AddBuff(BuffID.OnFire, 140);

		}

		public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly;
		}

		public override void Kill(int timeLeft)
		{

			Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(rorAudio.SFX_BottleSmash, Projectile.position);
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Glass, 0f, 0f, 100, default, 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 40; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 150, default, 1.5f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
				Main.dust[dustIndex].velocity *= 3f;
			}

			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.MolotovFire2, Projectile.damage, Projectile.knockBack, Main.myPlayer);

			Projectile.tileCollide = true;
			// Set to transparant. This projectile technically lives as  transparant for about 3 frames
			Projectile.alpha = 255;
			// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
			Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
			Projectile.damage = 19;
			Projectile.knockBack = 2f;
			Projectile.penetrate = 1;
			Projectile.ownerHitCheck = false;

			// These gores work by simply existing as a texture inside any folder which path contains "Gores/"
			int RumGore1 = Mod.Find<ModGore>("RumGore1").Type;
			int RumGore2 = Mod.Find<ModGore>("RumGore2").Type;
			int RumGore3 = Mod.Find<ModGore>("RumGore3").Type;

			IEntitySource entitySource = Projectile.GetSource_Death();

			for (int i = 0; i < 3; i++)
			{
				Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore1);
				Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore2);
				Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore3);

			}
		}
	}
}

