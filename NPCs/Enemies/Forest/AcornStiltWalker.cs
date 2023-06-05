using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.Forest
{

	public class AcornStiltWalker : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Acorn Stilt Walker");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GiantWalkingAntlion];

			var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 60;
			NPC.height = 108;
			NPC.damage = 13;
			NPC.defense = 2;
			NPC.lifeMax = 110;
			NPC.value = buyPrice(0, 0, 9, 65);
			NPC.aiStyle = 3;
			NPC.HitSound = SoundID.DD2_KoboldFlyerHurt;
			NPC.DeathSound = SoundID.DD2_KoboldFlyerDeath;
			NPC.netAlways = true;
			NPC.netUpdate = true;

			AIType = NPCID.GiantWalkingAntlion;
			AnimationType = NPCID.GiantWalkingAntlion;

		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.Player.ZoneForest && Main.dayTime && !spawnInfo.PlayerSafe ? 0.15f : 0f;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
								BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Being in the forest gets a bit boring sometimes, so this Acorn found some tree branches and stuck it to its legs! "),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore1").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore1").Type, 1f);

				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore3").Type, 1f);

			}

			for (int i = 0; i < 10; i++)
			{

				Vector2 speed = Main.rand.NextVector2Square(1f, 1f);

				var d = Dust.NewDustPerfect(NPC.position, DustID.t_LivingWood, speed * 5, Scale: 1.5f);
				;
				d.noGravity = true;
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{

			npcLoot.Add(ItemDropRule.Common(ItemID.Acorn, 1, 2, 4));
			npcLoot.Add(ItemDropRule.Common(ItemID.Wood, 2, 4, 6));
			npcLoot.Add(ItemDropRule.Common(ItemID.LivingWoodWand, 40, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ItemID.PortableStool, 42, 1, 1));

		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			// Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
			// Common use is applying buffs/debuffs:

			for (int d = 0; d < 30; d++)
				Dust.NewDust(target.position, target.width, target.height, DustID.t_LivingWood, 0f, 0f, 150, default, 1.5f);

		}

		public override void OnSpawn(IEntitySource source)
		{
			SoundEngine.PlaySound(rorAudio.SFX_Acorn);

		}
	}
}

