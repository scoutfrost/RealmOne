using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria.GameContent.ItemDropRules;
using System.Drawing.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RealmOne.NPCs.Enemies.Corruption
{
    public class EbonScreecher : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;

            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.TownCritter[NPC.type] = true;

        }

        public override void SetDefaults()
        {

            NPC.width = 26;
            NPC.height = 28;
            NPC.height = 38;
            NPC.defense = 10;
            NPC.damage = 15;
            NPC.lifeMax = 150;
            NPC.aiStyle = NPCAIStyleID.Vulture;
            NPC.damage = 15;

            NPC.knockBackResist = 0.50f;

            NPC.friendly = false;
            NPC.townNPC = false ;

            NPC.HitSound = SoundID.ChesterOpen;
            NPC.DeathSound = SoundID.NPCHit23;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneCorrupt)
            {
                return SpawnCondition.Corruption.Chance * 0.12f;
            }
            return 0;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.14f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction;

            if (NPC.Center.Y >= NPC.ai[0])
            {
                NPC.ai[1] = -1f;
                NPC.netUpdate = true;
                NPC.netAlways = true;
            }
            if (NPC.Center.Y <= NPC.ai[0] - 2f)
            {
                NPC.ai[1] = 1f;
                NPC.netUpdate = true;
                NPC.netAlways = true;
            }
            NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y + (0.006f * NPC.ai[1]), -.5f, .5f);

            if (NPC.velocity.X / 1 >= 1 || NPC.velocity.X / 1 <= -1)
            {
                Dust.NewDustPerfect(NPC.Center, DustID.CorruptionThorns, Scale: 0.5f, Alpha: 120);
            }

           
           
            /*  int npcaway = (int)Math.Sqrt((NPC.Center.X - target.Center.X) * (NPC.Center.X - target.Center.X) + (NPC.Center.Y - target.Center.Y) * (NPC.Center.Y - target.Center.Y));
              if (npcaway <= 400 || NPC.life < NPC.lifeMax && !target.dead)
              {
                  NPC.Transform(ModContent.NPCType<EbonScreecherBAD>());
              }
            */
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 drawOrigin = NPC.frame.Size() / 2;
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            return true;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore3").Type, 1.3f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore3").Type, 1.3f);



            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptionThorns);
            }



        }
        public override bool CheckDead()
        {
            for (int i = 0; i < Main.rand.Next(1, 1); i++)
            {
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<EbonScreecherBAD>(), ai3: 1).scale = 1f;
            }
            return true;
        }
        public override void OnKill()
        {

            for (int i = 0; i < 23; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.CursedTorch, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }
        }
     }
     
    }

