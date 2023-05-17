using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Explosive
{
	public class BattleboatBombProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Battle Boat Bomb");

		}
		public override void SetDefaults()
		{

			// while the sprite is actually bigger than 15x15, we use 15x15 since it lets the projectile clip into tiles as it bounces. It looks better.
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.hostile = true;
			Projectile.penetrate = 3;
			Projectile.tileCollide = false;
			// 4 second defuse
			Projectile.timeLeft = 4;
			Projectile.localNPCHitCooldown = 10;
			// These 2 help the projectile hitbox be centered on the projectile sprite.
			DrawOffsetX = 5;
			DrawOriginOffsetY = 5;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true;
		}

		public override void AI()
		{

			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
			{
				Projectile.tileCollide = false;
				// Set to transparant. This projectile technically lives as  transparant for about 3 frames
				Projectile.alpha = 255;
				// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
				Projectile.position.X = Projectile.position.X + Projectile.width / 2;
				Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
				Projectile.width = 480;
				Projectile.height = 480;
				Projectile.position.X = Projectile.position.X - Projectile.width / 2;
				Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
				Projectile.damage = 130;
				Projectile.knockBack = 8f;
				Projectile.penetrate = 3;
				Projectile.ownerHitCheck = false;
				Projectile.localNPCHitCooldown = 10;

			}
			else
				// Smoke and fuse dust spawn.
				if (Main.rand.NextBool(2))
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
				Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
				Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy(Projectile.rotation, default) * 1.1f;
			}

			return;
		}

		public override void Kill(int timeLeft)
		{
			Projectile.penetrate = 3;
			Projectile.friendly = true;
			Projectile.hostile = true;
			Projectile.ownerHitCheck = false;
			// If we are the original projectile, spawn the 5 child projectiles

			//    SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
			// Smoke Dust spawn
			for (int i = 0; i < 50; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 3f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 1.7f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare, 0f, 0f, 100, default, 1f);
				Main.dust[dustIndex].velocity *= 2f;
			}
			// Large Smoke Gore spawn

			// reset size to normal width and height.
			Projectile.position.X = Projectile.position.X + Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;

			// TODO, tmodloader helper method
			{
				int explosionRadius = 150;
				//if (projectile.type == 29 || projectile.type == 470 || projectile.type == 637)
				//{
				//  explosionRadius = 15;
				//}
				int minTileX = (int)(Projectile.position.X / 16f - explosionRadius);
				int maxTileX = (int)(Projectile.position.X / 16f + explosionRadius);
				int minTileY = (int)(Projectile.position.Y / 16f - explosionRadius);
				int maxTileY = (int)(Projectile.position.Y / 16f + explosionRadius);
				if (minTileX < 0)
					minTileX = 0;
				if (maxTileX > Main.maxTilesX)
					maxTileX = Main.maxTilesX;
				if (minTileY < 0)
					minTileY = 0;
				if (maxTileY > Main.maxTilesY)
					maxTileY = Main.maxTilesY;
				bool canKillWalls = false;
				for (int x = minTileX; x <= maxTileX; x++)
					for (int y = minTileY; y <= maxTileY; y++)
					{
						float diffX = Math.Abs(x - Projectile.position.X / 16f);
						float diffY = Math.Abs(y - Projectile.position.Y / 16f);
						double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
						if (distance < explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].WallType == 0)
						{
							canKillWalls = true;
							break;
						}
					}
			}
		}
	}
}

