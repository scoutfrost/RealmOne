using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Utilities;
using RealmOne.Items;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using RealmOne.Common.Systems;
using Terraria.GameContent.ItemDropRules;
using System.IO;
using RealmOne.BossBars;
using RealmOne.NPCs.Enemies;
using Terraria.DataStructures;
using System.Collections.Generic;
using System;
using RealmOne.Bosses;
using RealmOne.Items.BossBags;

namespace RealmOne.Bosses
{
    [AutoloadBossHead]
    internal class SquirmoHead : WormHead
    {
      /*  public bool SpawnedMinions
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }*/
        public ref float RemainingShields => ref NPC.localAI[2];

        public override int BodyType => ModContent.NPCType<SquirmoBody>();

        public override int TailType => ModContent.NPCType<SquirmoTail>();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirmo");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DiggerHead];


            NPCID.Sets.MPAllowedEnemies[Type] = true;
            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            // Specify the debuffs it is immune to
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.Slow,
                    BuffID.OnFire3,
                    BuffID.Slimed,
                    BuffID.Confused // Most NPCs have this
				}
            };

            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            // Influences how the NPC looks in the Bestiary

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                CustomTexturePath = "RealmOne/NPCs/Enemies/ArtyWorm", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
                Position = new Vector2(40f, 28f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f,
                PortraitScale = 1.5f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }

        public override void SetDefaults()
        {
            // Head is 10 defence, body 20, tail 30.
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 2800;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 30;
            NPC.defense = 4;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
            NPC.BossBar = ModContent.GetInstance<SquirmoBar>();
            NPC.dontTakeDamageFromHostiles = true;
            NPC.scale = 2f;
            NPC.noTileCollide = true;
            NPC.SpawnWithHigherTime(30);
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.boss = true;
            NPC.npcSlots = 10f;


            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/SquirmoDrip");
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(3000 * bossLifeScale);
            NPC.damage = 40;
            NPC.defense = 5;

            if (Main.masterMode)
            {
                NPC.lifeMax = (int)(3250 * bossLifeScale);
                NPC.damage = 60;
                NPC.defense = 6;
            }
        }
        /* public static int MinionType()
         {
             return ModContent.NPCType<MegaSquirmHead>();
         }*/
        /*   public static int MinionCount()
           {
               int count = 2;

               if (Main.expertMode)
               {
                   count += 2; // Increase by 5 if expert or master mode
               }


               return count;
           }*/
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
            

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("The dreaded controller and ruler of all creepy crawlies of the soil. Attacks anything that touches a grain of dirt, without any care of destruction"),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
                return;
            }

            if (NPC.life <= 0)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                int SquirmoGoreBody = Mod.Find<ModGore>("SquirmoGore1").Type;
                int SquirmoGoreHead = Mod.Find<ModGore>("SquirmoGore2").Type;
                var entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4 , 5), Main.rand.Next(-4, 5)), SquirmoGoreBody);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreHead);
                }

                SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, NPC.Center);
            }
            for (int i = 0; i < 25; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SquirmoBossBag>()));


            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeables.SquirmoRelicItem>()));


            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());


        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedSquirmo, -1);

        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            
        }


        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
            return true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Slow;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
            int buffType1 = BuffID.OgreSpit;


            int timeToAdd = 4 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);


            int timeToAdd1 = 4 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType1, timeToAdd1);
        }
        public override void Init()
        {
            // Set the segment variance
            // If you want the segment length to be constant, set these two properties to the same value
            MinSegmentLength = 30;
            MaxSegmentLength = 30;

            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            worm.MoveSpeed = 14f;
            worm.Acceleration = 0.19f;
        }
        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void AI()
        {

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.2f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }
       //     SpawnMinions();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (attackCounter > 0)
                    attackCounter--; // tick down the attack counter.


                Player target = Main.player[NPC.target];


                // If the attack counter is 0, this NPC is less than 12.5 tiles away from its target, and has a path to the target unobstructed by blocks, summon a projectile.
                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 450 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(8));

                    int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 2, ModContent.ProjectileType<GlobGunProjectileHostile>(), 16, 1, Main.myPlayer);
                    Main.projectile[projectile].timeLeft = 60;
                    attackCounter = 250;
                    NPC.netUpdate = true;
                    Main.projectile[projectile].velocity *= 6f;
                    
                  
                }


            }

        }
      /*  private void SpawnMinions()
        {
            if (SpawnedMinions)
            {
                // No point executing the code in this method again
                return;
            }

            SpawnedMinions = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // Because we want to spawn minions, and minions are NPCs, we have to do this on the server (or singleplayer, "!= NetmodeID.MultiplayerClient" covers both)
                // This means we also have to sync it after we spawned and set up the minion
                return;
            }

            int count = MinionCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MegaSquirmHead>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                // Now that the minion is spawned, we need to prepare it with data that is necessary for it to work
                // This is not required usually if you simply spawn NPCs, but because the minion is tied to the body, we need to pass this information to it

                if (minionNPC.ModNPC is MegaSquirmHead minion)
                {
                    // This checks if our spawned NPC is indeed the minion, and casts it so we can access its variables
                    minion.ParentIndex = NPC.whoAmI; // Let the minion know who the "parent" is
                    minion.PositionIndex = i; // Give it the iteration index so each minion has a separate one, used for movement
                }

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }*/
    }
    internal class SquirmoBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirmo Body");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 20;
            NPC.defense = 2;
            NPC.scale = 2f;


            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
                return;
            }

            if (NPC.life <= 0)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                int SquirmoGoreBody = Mod.Find<ModGore>("SquirmoGore1").Type;
                int SquirmoGoreHead = Mod.Find<ModGore>("SquirmoGore2").Type;

                var entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreBody);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreHead);

                }

                SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, NPC.Center);
            }
            for (int i = 0; i < 18; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
        }
        public override void Init()
        {
            SquirmoHead.CommonWormInit(this);
        }
    }

    internal class SquirmoTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirmo Tail");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 17;
            NPC.defense = 2;
            NPC.scale = 2f;

            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers
                return;
            }

            if (NPC.life <= 0)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                int SquirmoGoreBody = Mod.Find<ModGore>("SquirmoGore1").Type;
                int SquirmoGoreHead = Mod.Find<ModGore>("SquirmoGore2").Type;
                var entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 2; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreBody);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreHead);
                }

                SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, NPC.Center);
            }
            for (int i = 0; i < 18; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                Dust d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
        }
        public override void Init()
        {
            SquirmoHead.CommonWormInit(this);
        }
    }
}


