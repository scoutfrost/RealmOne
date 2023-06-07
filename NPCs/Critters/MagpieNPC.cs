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
using Terraria.ModLoader.Utilities;
using RealmOne.Items.ItemCritter;

namespace RealmOne.NPCs.Critters
{
    public class MagpieNPC : ModNPC
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magpie");
            Main.npcFrameCount[NPC.type] = 5;
            Main.npcCatchable[NPC.type] = true;


            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { 
                Velocity = 1f 
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.CountsAsCritter[Type] = true;



        }

        public override void SetDefaults()
        {

            NPC.catchItem = (short)ModContent.ItemType<MagpieItem>();
            NPC.width = 24;
            NPC.height = 20;
            NPC.dontCountMe = true;

            NPC.damage = 1;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.34f;
            NPC.dontTakeDamageFromHostiles = true;

            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Bird;
            AIType = NPCID.Bird;
            AnimationType = NPCID.Bird;

        }
       
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MagpieGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MagpieGore2").Type, 1f);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayBirdCritter.Chance * 1.5f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface  ,


                new FlavorTextBestiaryInfoElement("A common backyard musician! This bird tends to make its calls during the morning and evening! Be careful, they tend to swoop atcha!"),


            });
        }
    }
}
