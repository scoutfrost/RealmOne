using Microsoft.Xna.Framework;
using RealmOne.BossBars;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
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
        //just some disclaimers

        int coinScatter; //Coin shotgun cooldown

        int groundPound; //Ground Pound cooldown

        int move; // Cooldown for moving (causes bouncing which is wanted)

        int move1; //Cooldown for hopping upwards when Coin Shotgun is happening

        bool coinAtk1 = false; //Is Coin Shotgun happening?


        public ref float RemainingShields => ref NPC.localAI[2];

        public int MinionMaxHealthTotal
        {
            get => (int)NPC.ai[1];
        }
        public int MinionHealthTotal { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Possessed Piggybank");
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 24;
            NPC.damage = 27;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.knockBackResist = 0.6f;
            NPC.value = Item.buyPrice(0, 2, 50, 50);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item59;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.noGravity = false;
            NPC.boss = true;
            AnimationType = -1;
            AIType = -1;
            NPC.BossBar = ModContent.GetInstance<PiggyBossBar>();
            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/PiggyPatrol");


        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
            NPCLoader.blockLoot.Add(ItemID.Heart);
        }

        public override void FindFrame(int frameHeight)
        {

            if (NPC.frameCounter < 5)
            {
                NPC.frame.Y = 1 * frameHeight;
            }
            else if (NPC.frameCounter < 10)
            {
                NPC.frame.Y = 2 * frameHeight;
            }
            else if (NPC.frameCounter < 15)
            {
                NPC.frame.Y = 3 * frameHeight;
            }
            else if (NPC.frameCounter < 20)
            {
                NPC.frame.Y = 4 * frameHeight;
            }
            else if (NPC.frameCounter < 25)
            {
                NPC.frame.Y = 4 * frameHeight;
            }
            else if (NPC.frameCounter < 30)
            {
                NPC.frame.Y = 5 * frameHeight;
            }

            else
            {
                NPC.frameCounter = 0;
            }
        }


        public override void AI()
        {
            NPC.TargetClosest(true);

            var t = Main.LocalPlayer;


            NPC.frameCounter++;

            Vector2 d = (t.Center - NPC.Center).SafeNormalize(Vector2.UnitX);

            if (t.dead)
            {
                if (move == 0)
                {
                    NPC.velocity -= d * 10;

                    move = 35;
                }

                NPC.EncourageDespawn(60);
                return;
            }



            if (move > 0)
            {
                move--;
            }
            if (move1 > 0)
            {
                move1--;
            }

            /*if (groundPound > 0)
            {
                groundPound--;
            }*/

            if (coinScatter > 0)
            {
                coinScatter--;
            }

            if (/*groundPound > 0 &&*/ coinAtk1 == false)
            {


                if (move == 0)
                {
                    NPC.velocity = d * 4;

                    move = 35;
                }
            }
            if (move == 33)
            {
                NPC.velocity.Y -= 6;
            }

            if (coinAtk1 == true)
            {
                Vector2 tt = t.Center - NPC.Center;

                NPC.rotation = tt.ToRotation();



                if (move1 == 0)
                {
                    NPC.velocity = d * 2;

                    move1 = 35;
                }
                if (move1 == 33)
                {
                    NPC.velocity.Y -= 9;
                }
            }
            else if (coinAtk1 == false)
            {
                NPC.spriteDirection = NPC.direction;
            }

            if (groundPound == 0)
            {
                groundPound = 0;
            }


            if (coinScatter == 0)
            {

                coinScatter = 500;
                NPC.knockBackResist = 1f;
                coinAtk1 = true;
            }
            if (coinScatter == 450)
            {
                SoundEngine.PlaySound(SoundID.Item59, NPC.position);
            }
            if (coinScatter == 385)
            {
                SoundEngine.PlaySound(SoundID.Item59, NPC.position);
            }
            if (coinScatter == 380)
            {
                SoundEngine.PlaySound(SoundID.Item59, NPC.position);
            }
            if (coinScatter == 375)
            {
                SoundEngine.PlaySound(SoundID.Item59, NPC.position);
            }
            if (coinScatter == 365)
            {
                Vector2 direction1 = d.RotatedBy(MathHelper.ToRadians(8));
                Vector2 direction2 = d.RotatedBy(MathHelper.ToRadians(-8));
                Vector2 direction3 = d.RotatedBy(MathHelper.ToRadians(5));
                Vector2 direction4 = d.RotatedBy(MathHelper.ToRadians(-5));
                Vector2 direction5 = d.RotatedBy(MathHelper.ToRadians(0));

                int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction1 * 7, (ModContent.ProjectileType<GoldGold>()), NPC.damage, 1, Main.myPlayer);
                int projectile1 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction2 * 7, (ModContent.ProjectileType<GoldGold>()), NPC.damage, 1, Main.myPlayer);
                int projectile2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction3 * 7, (ModContent.ProjectileType<GoldGold>()), NPC.damage, 1, Main.myPlayer);
                int projectile3 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction4 * 7, (ModContent.ProjectileType<GoldGold>()), NPC.damage, 1, Main.myPlayer);
                int projectile5 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction5 * 7, (ModContent.ProjectileType<GoldGold>()), NPC.damage, 1, Main.myPlayer);

                SoundEngine.PlaySound(SoundID.Coins, NPC.position);
                SoundEngine.PlaySound(SoundID.Item59, NPC.position);
                coinScatter = 300;
                coinAtk1 = false;
                NPC.rotation = 0f;
                NPC.knockBackResist = 0.6f;
            }

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.09f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				new FlavorTextBestiaryInfoElement("An evil piggy bank that got a bit too empty of money through its lifetime. Even though it has wings, it cannot fly (lol)."),

				
            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
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
            int buffType = BuffID.Midas;
            int timeToAdd = 5 * 60;
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
            coinScatter = 300;

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

