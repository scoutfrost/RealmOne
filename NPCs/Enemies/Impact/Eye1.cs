using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Impact
{
	public class Eye1 : ModNPC
	{
		static Asset<Texture2D> glowmask;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye1");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];

			var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
			glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 15;
			NPC.damage = 15;
			NPC.defense = 1;
			NPC.lifeMax = 75;
			NPC.value = buyPrive(0, 0, 4, 55);
			NPC.aiStyle = NPCAIStyleID.DemonEye;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_ElectricDeath");
			NPC.netAlways = true;
			NPC.netUpdate = true;
			AIType = NPCID.DemonEye;
			AnimationType = NPCID.DemonEye;

		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * 0.18f;
		}

		public override void AI()
		{
			Lighting.AddLight(NPC.position, r: 0.1f, g: 0.2f, b: 1.0f);
		}
		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Color color = GetAlpha(Color.LightBlue) ?? Color.LightBlue;

			if (NPC.IsABestiaryIconDummy)
				color = Color.LightBlue;

			Main.EntitySpriteDraw(glowmask.Value, NPC.Center - screenPos + new Vector2(0, 0), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
								BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Purged and paralyzed, this slime is nothing but artificial plasma."),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode != NetmodeID.Server)
			{
				if (NPC.life <= 0)
				{
					// These gores work by simply existing as a texture inside any folder which path contains "Gores/"

					Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EeyeGore1").Type, 1f);
					Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EeyeGore2").Type, 1f);

				}
			}

			for (int i = 0; i < 10; i++)
			{

				Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

				var d = Dust.NewDustPerfect(NPC.position, DustID.Electric, speed * 5, Scale: 2f);
				;
				d.noGravity = true;
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 1, 1, 3));
			npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 5));

		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			// Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
			// Common use is applying buffs/debuffs:

			int buffType = BuffID.Electrified;
			// Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

			int timeToAdd = 2 * 60; //This makes it 5 seconds, one second is 60 ticks
			target.AddBuff(buffType, timeToAdd);
		}
	}
}
