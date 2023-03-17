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

namespace RealmOne.NPCs.Critters
{
    public class AquaSwishSnail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AquaSwish Snail");
            Main.npcFrameCount[NPC.type] = 6;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[Type] = true;

        }

        public override void SetDefaults()
        {

            NPC.catchItem = (short)ModContent.ItemType<AquaSwishSnailItem>();
            NPC.width = 24;
            NPC.height = 20;
            NPC.dontCountMe = true;

            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.34f;
            NPC.dontTakeDamageFromHostiles = true;

            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Snail;
            AIType = NPCID.Snail;
            AnimationType = NPCID.Snail;

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterSnailGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterSnailGore2").Type, 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) =>
            
           !spawnInfo.Player.ZoneForest
            && spawnInfo.Player.ZoneOverworldHeight
            && !spawnInfo.Player.ZoneRain
            && Main.dayTime
            && Main.raining
            && !spawnInfo.PlayerSafe ? 0.5f : 0f;

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0f, g: 0.1f, b: 0.4f); ;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("AquaSwishSnail_Glow").Value,
                NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY),
                NPC.frame,
                Color.White,
                NPC.rotation,
                NPC.frame.Size() / 2,
                NPC.scale,
                SpriteEffects.None, 0
            );

        }
       


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,


                new FlavorTextBestiaryInfoElement("From the constant dew of the morning moisture, some species of snails have been blended with a zealous and unusual source of water magic!"),


            });
        }
    }
}
