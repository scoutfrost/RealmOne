using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Misc;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using RealmOne.Items.Weapons.Ranged;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class ImpactTurret : ModNPC
    {

        static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact Turret");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 30;
            NPC.damage = 5;
            NPC.lifeMax = 27;
            NPC.aiStyle = NPCAIStyleID.TeslaTurret;
            NPC.value = 50f;
            AIType = NPCID.MartianTurret;
            NPC.HitSound = SoundID.NPCHit53;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_ElectricDeath");
            AnimationType = NPCID.MartianTurret;




        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.LightBlue) ?? Color.LightBlue;

            if (NPC.IsABestiaryIconDummy)
                color = Color.LightBlue;

            Main.EntitySpriteDraw(glowmask.Value, NPC.Center - screenPos + new Vector2(0, 0), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
        }
        // public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneForest && spawnInfo.Player.ZoneRockLayerHeight && spawnInfo.Player.ZoneNormalUnderground && spawnInfo.Water ? 1f : 0f;


        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneForest ? 0.08f : 0f;

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("An unknown device that teleports around the world. Almost scanning as an envoy for something much more larger in hiarachy.Be careful it can deal a lot of damage!"),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 1, 2, 3));

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TurretGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TurretGore2").Type, 1f);

            }


            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Electric, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.1f, g: 0.2f, b: 1.1f);
        }

    }
}
