using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Misc;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using RealmOne.Common.Systems;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace RealmOne.NPCs.Enemies
{
    public class LightbulbBouncer : ModNPC
    {
        static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb Bouncer");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.FloatyGross];
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.NavajoWhite) ?? Color.NavajoWhite;

            if (NPC.IsABestiaryIconDummy)
                color = Color.NavajoWhite;

            Main.EntitySpriteDraw(glowmask.Value, NPC.Center - screenPos + new Vector2(0, 0), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 32;
            NPC.noGravity = true;
            NPC.damage = 10;
            NPC.defense = 1;
            NPC.lifeMax = 48;

            NPC.value = 68f;
            NPC.aiStyle = NPCAIStyleID.DemonEye;
            NPC.HitSound = SoundID.DD2_WitherBeastHurt;
            NPC.DeathSound = SoundID.DD2_WitherBeastHurt;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            AnimationType = NPCID.FloatyGross;

        }
        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.20f;
        }
        private int dustTimer;

        public override void AI()
        {


            // Leave a dust trail behind the enemy
            Vector2 center = NPC.Center;
            for (int j = 0; j < 60; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.Teleporter, 0f, 0f, 100, default, 0.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;


            }


            Lighting.AddLight(NPC.position, r: 1.4f, g: 1.9f, b: 0.1f);
        }
      
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Light is super important, especially when its night, but when these deceptive floating bulbs are around, you know they're up to no good."),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
        }
        public override void HitEffect(int hitDirection, double damage)
        {

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("LightbulbBouncerGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("LightbulbBouncerGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("LightbulbBouncerGore3").Type, 1f);

            }
            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Teleporter, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Electric, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LightbulbLiquid>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Glass, 1, 1, 5));


        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Blackout;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 2 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }

    }
}
