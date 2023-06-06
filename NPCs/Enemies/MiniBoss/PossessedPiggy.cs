using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.MiniBoss
{

	public class PossessedPiggy : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Possessed Piggybank");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GiantWalkingAntlion];
			NPCID.Sets.TrailCacheLength[NPC.type] = 3;
			NPCID.Sets.TrailingMode[NPC.type] = 0;
			var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults()
		{
			NPC.width = 30;
			NPC.height = 26;
			NPC.damage = 27;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.value = buyPrice(0, 2, 37, 50);
			NPC.aiStyle = 25;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.Item59;
			NPC.netAlways = true;
			NPC.netUpdate = true;
            NPC.boss = true;

            AIType = NPCID.Mimic;
			AnimationType = NPCID.GiantWalkingAntlion;
			Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/PiggyPatrol");

            
        }
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * 0.09f;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
								BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("An evil piggy bank that got a bit too empty of money through its lifetime. Even though it has wings, it cannot fly (lol)."),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
		}
		public override void HitEffect(NPC.HitInfo hit)
		{

			if (NPC.life <= 0)
			{
				// These gores work by simply existing as a texture inside any folder which path contains "Gores/"

				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore1").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore2").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore3").Type, 1f);

			}

			for (int i = 0; i < 20; i++)
			{

				Vector2 speed = Main.rand.NextVector2Square(1f, 1f);

				var d = Dust.NewDustPerfect(NPC.position, DustID.DungeonPink, speed * 5, Scale: 1.5f);
				;
				d.noGravity = true;
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PiggyPorcelain>(), 1, 4, 5));
			npcLoot.Add(ItemDropRule.Common(ItemID.PiggyBank, 20, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ItemID.MoneyTrough, 30, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 5, 6));

		}

		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			// Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
			// Common use is applying buffs/debuffs:

			int buffType = BuffID.Midas;
			// Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

			int timeToAdd = 5 * 60; //This makes it 5 seconds, one second is 60 ticks
			target.AddBuff(buffType, timeToAdd);

			CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height), new Color(234, 129, 178, 110), "That's my money, fool!", false, false);

		}
		public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
		{
			CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(234, 129, 178, 180), "Ow that hurt!", false, false);

		}

		public override void OnKill()
		{
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue($"[i:{ItemID.GoldCoin}]The angry lil loot scavenger has been shattered!![i:{ItemID.GoldCoin}]"), 71, 229, 231);

            }
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedPiggy, -1);

			if (Main.netMode != NetmodeID.Server)
				CombatText.NewText(new Rectangle((int)NPC.position.X + 30, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(234, 129, 178, 190), "You got lucky that time!", false, false);
		}
		public override void OnSpawn(IEntitySource source)
		{
			Player player = Main.player[NPC.target];

			SoundEngine.PlaySound(SoundID.Item59);
			CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(234, 129, 178, 180), "Oink!! Oi buddy, over here!!", false, false);

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit)
		{
			CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(258, 27, 124, 200), "I got your little friends as well!", false, false);

		}
	}
}

