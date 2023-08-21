using Microsoft.Xna.Framework;
using RealmOne.Items.Food.FarmFood;
using RealmOne.Items.ItemCritter;
using RealmOne.Items.Others;
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
    public class Berryback : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berryback");

            Main.npcFrameCount[NPC.type] = 9;

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.CountsAsCritter[Type] = true;
            Main.npcCatchable[NPC.type] = true;


        }

        public override void SetDefaults()
        {
            NPC.catchItem = (short)ModContent.ItemType<BerrybackItem>();

            NPC.width = 23;
            NPC.height = 20;
            NPC.defense = 5;
            NPC.lifeMax = 5;
            NPC.value = Item.buyPrice(0, 0,6, 0);
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.dontCountMe = true;
            NPC.npcSlots = 0;


            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Turtle;
            AnimationType = NPCID.Turtle;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };


        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 1f : 0f;
            }
            return 0f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
               

                new FlavorTextBestiaryInfoElement("The mix of bacteria on its shell makes a ever-growing field of bright, colourful flowers to grow on it!!"),

            });
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FloralDelight>(), 6, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Blinkroot, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Waterleaf, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Daybloom, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Moonglow, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Shiverthorn, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Fireblossom, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));







        }

        public override void HitEffect(NPC.HitInfo hit)
        {

            for (int i = 0; i < 20; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Grass, speed * 5, Scale: 2f);
                ;
                d.noGravity = false;
            }
        }


    }
}
