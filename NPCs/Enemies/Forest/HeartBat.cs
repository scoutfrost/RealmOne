using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Forest
{
	public class HeartBat : ModNPC
	{
		static Asset<Texture2D> glow;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heartbat");
			Main.npcFrameCount[NPC.type] = 5;
			//     Main.npcCatchable[NPC.type] = true;

			var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f
			};
			glow = ModContent.Request<Texture2D>(Texture + "_Glow");

			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

			//     NPCID.Sets.CountsAsCritter[Type] = true;

		}

		public override void SetDefaults()
		{

			//  NPC.catchItem = (short)ModContent.ItemType<ThornyDevilItem>();
			NPC.width = 20;
			NPC.height = 20;
			//  NPC.dontCountMe = true;

			NPC.damage = 8;
			NPC.defense = 5;
			NPC.lifeMax = 58;
			NPC.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
			NPC.DeathSound = SoundID.DD2_WitherBeastDeath;

			NPC.knockBackResist = 0.34f;
			// NPC.dontTakeDamageFromHostiles = true;

			//     NPC.npcSlots = 0;
			NPC.aiStyle = NPCAIStyleID.Bat;
			AIType = NPCID.CaveBat;
			AnimationType = NPCID.CaveBat;
			NPC.lifeRegen = 5;

		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Color color = GetAlpha(Color.White) ?? Color.White;

			if (NPC.IsABestiaryIconDummy)
				color = Color.White;

			Main.EntitySpriteDraw(glow.Value, NPC.Center - screenPos + new Vector2(0, 3), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
		}
		public override void HitEffect(NPC.HitInfo hit)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HeartbatGore1").Type, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HeartbatGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HeartbatGore3").Type, 1f);

            }
        }

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.PlayerSafe)

				return SpawnCondition.Underground.Chance * 0.20f;
			return SpawnCondition.Cavern.Chance * 0.20f;
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{

			target.AddBuff(BuffID.Rabies, 180);
			target.AddBuff(BuffID.Regeneration, 120);

		}

		/*  public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
          {
              drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
              var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
              spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
              return false;
          }*/

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 15, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ItemID.Heart, 1, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ItemID.Heart, 1, 1, 1));

			npcLoot.Add(ItemDropRule.Common(ItemID.LifeforcePotion, 10, 1, 1));
			npcLoot.Add(ItemDropRule.Common(ItemID.RegenerationPotion, 6, 1, 1));

		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground ,

				new FlavorTextBestiaryInfoElement("Crystalised with pure life, this fortunate bat can regenerate its health super fast"),

			});
		}
	}
}
