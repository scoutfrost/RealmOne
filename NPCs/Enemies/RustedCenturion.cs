using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Misc;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using RealmOne.Items.Weapons.Ranged;
using RealmOne.Items.Weapons.Melee;
using RealmOne.Items;
using RealmOne.Common;
using Terraria.Audio;
using RealmOne.Projectiles;
using RealmOne.Items.Tools;

namespace RealmOne.NPCs.Enemies
{
    public class RustedCenturion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusted Centurion");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BoneThrowingSkeleton2];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 42;
            NPC.damage = 30;
            NPC.defense = 5;
            NPC.lifeMax = 100;
            NPC.value = 80f;
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.DD2_SkeletonHurt;
            NPC.DeathSound = SoundID.DD2_SkeletonDeath;
            AIType = NPCID.ArmoredSkeleton;
            AnimationType = NPCID.BoneThrowingSkeleton2;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.2f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Battered, rusted, revolutionised. This centurion has been fighting in The Ancient War before evolution. The bones of this centurion show nothing but history and moss."),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PompeiisPull>(), 36, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoopyGrass>(), 2, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.IronBar, 11, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.Javelin, 8, 30, 40));
            npcLoot.Add(ItemDropRule.Common(ItemID.LeadBar, 11, 3, 6));

            npcLoot.Add(ItemDropRule.Common(ItemID.MudBlock, 2, 3, 6));

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 19; i++)
            {

                Vector2 speed = Main.rand.NextVector2Square(2f, 2f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Bone, speed * 5, Scale: 1.5f); ;
                d.noGravity = true;

            }

            if (NPC.life <= 0)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"
                int CenturionGore1 = Mod.Find<ModGore>("CenturionGore1").Type;
                int CenturionGore2 = Mod.Find<ModGore>("CenturionGore2").Type;
                int CenturionGore3 = Mod.Find<ModGore>("CenturionGore3").Type;

                var entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), CenturionGore1);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), CenturionGore2);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), CenturionGore3);

                }

            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Poisoned;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 5 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);

            int buffType1 = BuffID.BrokenArmor;


            int timeToAdd1 = 20 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType1, timeToAdd1);
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.TargetClosest(true);
            if ((player.Center - NPC.Center).Length() < 800)
            {
                float projectileSpeed = 8f;
                int damage = 65;
                float knockBack = 3;
                int type = ProjectileID.JavelinHostile;
                Vector2 velocity = Vector2.Normalize(new Vector2(player.position.X + player.width / 2, player.position.Y + player.height / 2) -
                    new Vector2(NPC.position.X + NPC.width / 2, NPC.position.Y + NPC.height / 2)) * projectileSpeed;


            }
        }
    }
}
