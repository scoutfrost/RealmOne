using Microsoft.Xna.Framework;
using RealmOne.Items.BossSummons;
using RealmOne.Items.Misc;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Forest
{
    public class ZombieFarmer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zombie Farmer");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 42;
            NPC.damage = 18;
            NPC.defense = 1;
            NPC.lifeMax = 72;
            NPC.value = Item.buyPrice(0, 0, 2, 35);
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            NPC.netAlways = true;
            NPC.netUpdate = true;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneForest)
                return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
            return base.SpawnChance(spawnInfo);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A withered and undead zombie, looking for its next meal to plant in its fields"),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.Common(ItemID.Hay, 1, 6, 10));
            npcLoot.Add(ItemDropRule.Common(ItemID.Sickle, 40, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.GrassSeeds, 3, 1, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wheat>(), 2, 5, 8));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SquirmoSummon>(), 60, 1, 1));

        }

        public override void HitEffect(NPC.HitInfo hit)
        {

            if (Main.netMode != NetmodeID.Server)
            {
                if (NPC.life <= 0)
                {
                    // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore4").Type, 1f);

                }
            }

            for (int i = 0; i < 12; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Hay, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }
        }
    }
}