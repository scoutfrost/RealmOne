using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using RealmOne.Tiles.Blocks;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Critters.Farm
{
    public class HoneyHare : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Hare");

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GoldBunny];

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.CountsAsCritter[Type] = true;

        }

        public override void SetDefaults()
        {
            NPC.width = 23;
            NPC.height = 20;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.value = Item.buyPrice(0, 0, 5, 0);
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.HitSound = SoundID.NPCHit1;

            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Bunny;
            AnimationType = NPCID.Bunny;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };


        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 1.5f : 0f;
            }
            return 0f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("This bunny got a bit too curious hanging around in the farm, its got a bucket full of honey wedged onto its head, and a beehive on its back!!??"),

            });
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HoneyBucket, 5, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.BottomlessHoneyBucket, 25, 1, 1));

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            
                if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore2").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore4").Type, 1f);


                }

            
            for (int i = 0; i < 20; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Honey, speed * 5, Scale: 2f);
                ;
                d.noGravity = false;
            }
        }

        public override void OnKill()
        {
            Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ProjectileID.BeeHive, 10, 1);

        }
    }
}
