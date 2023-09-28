using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.BloodMoon
{
    public class ArteryCarrion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;

        }

		public override void SetDefaults()
		{
			NPC.width = 16;
			NPC.height = 21;
			NPC.damage = 14;
			NPC.defense = 3;
			NPC.lifeMax = 80;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f; 
			NPC.HitSound = SoundID.NPCHit19;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.noGravity = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer && Main.bloodMoon ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

		private double Timer;
		private double SinThing;
		private bool CanShoot;
        public override void OnSpawn(IEntitySource source)
        {
			Timer = 0;
        }
        public override void AI()
        {

			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
			}

			Timer++;
			if (Timer % 15 == 0)
				SinThing++;
			Player player = Main.player[NPC.target];
			Vector2 TargetLocation = new Vector2(player.position.X + ((float)Math.Sin(SinThing)*100), player.position.Y - 150 - ((float)Math.Sin(SinThing) *10));

			float speed = 9f;
			float inertia = 30f;
			Vector2 direction = TargetLocation - NPC.Center;
			direction.Normalize();
			direction *= speed;
			NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;

			NPC.rotation = NPC.velocity.X * 0.2f;
			var entitySource = NPC.GetSource_FromThis();
			if (Vector2.Distance(TargetLocation, NPC.Center) <= 130)
			{
                if (Timer % 60 == 0)
                {
					var projectile = Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(0f, 2f), ModContent.ProjectileType<ArteryCarrionProjectile>(), 10, 0f);
				}
					

			} 
				
			FindFrame(44);
		}

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Blood, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1));
			}
		}
        private int AnimFrameCount;
		private int AnimTimer;
		public override void FindFrame(int frameHeight)
		{
			AnimTimer++;
			
			if(AnimTimer%10==0)
				AnimFrameCount++;
			if (AnimFrameCount == 8)
			{
				AnimFrameCount = 1;
			}
			NPC.frame.Y = NPC.frame.Height * AnimFrameCount;
		}
	}

	public class ArteryCarrionProjectile :ModProjectile
    {
		public override void SetDefaults()
		{
			Projectile.width = 12; // The width of projectile hitbox
			Projectile.height = 12; // The height of projectile hitbox
			Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = true; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 5; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.damage = 10;
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
			

			; // Act exactly like default Bullet
		}
        public override void OnSpawn(IEntitySource source)
        {
			SoundEngine.PlaySound(SoundID.NPCHit13, Projectile.position);
		}
        public override void AI()
        {
			Projectile.velocity.Y += .1f;
			Projectile.rotation += (float)Math.PI / 12;
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for(int i = 0; i < 10; i++)
            {
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-1,1), Main.rand.Next(-1, 1));
			}
			Projectile.Kill();
			return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1));
			}
			
		}
    }
}
