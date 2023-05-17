using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.IO;
using ReLogic.Content;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using RealmOne.Items.ItemCritter;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Critters
{
    public class WaterLizard : ModNPC
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Lizard");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GoldfishWalker];
            Main.npcCatchable[NPC.type] = true;


            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.CountsAsCritter[Type] = true;



        }

        public override void SetDefaults()
        {

            NPC.catchItem = (short)ModContent.ItemType<WaterLizardCritter>();
            NPC.width = 40;
            NPC.height = 20;
            NPC.dontCountMe = true;

            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 75;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.34f;
            NPC.dontTakeDamageFromHostiles = true;

            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Passive;
            AnimationType = NPCID.GoldfishWalker;

        }


        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterLizardGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterLizardGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterLizardGore3").Type, 1f);

            }

            for (int i = 0; i < 18; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Water, speed * 5, Scale: 1.5f); ;
                d.noGravity = true;

            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.worldSurface && spawnInfo.Player.ZoneRain && spawnInfo.Player.ZoneForest && !Main.dayTime && !spawnInfo.PlayerSafe ? 0.5f : 0f;

        int Watertimer = 0;

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.02f, g: 0.7f, b: 1.1f);
            Watertimer++;

            if (Watertimer == 9)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water);
                Main.dust[d].scale = 1.2f;
                Main.dust[d].velocity *= 0.6f;
                Main.dust[d].noLight = false;

                Watertimer = 0;
            }
        }



        /*  public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
          {
              drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
              var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
              spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
              return false;
          }*/


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,


                new FlavorTextBestiaryInfoElement("Bred from the lush and damp waters of the ponds, this overly large amphibian seems to walk around when its nice and wet."),


            });
        }
    }
}
