﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using RealmOne.Items.Misc;
using Terraria.GameContent.ItemDropRules;
using RealmOne.Items.Placeables;
using RealmOne.Items.ItemCritter;

namespace RealmOne.NPCs.Critters.Farm
{
    public class OldSnail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grandfather Snail");

            Main.npcCatchable[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 7;

            NPCID.Sets.CountsAsCritter[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 16;
            NPC.damage = 0;
            NPC.defense = 5;
            NPC.lifeMax = 5;
            NPC.value = 0f;
            NPC.knockBackResist = 0f;
            NPC.dontCountMe = true;
            NPC.catchItem = (short)ModContent.ItemType<TatteredBarrelItem>();
            NPC.catchItem = (short)ModContent.ItemType<OldSnailItem>();
            NPC.aiStyle = NPCAIStyleID.Snail;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            AnimationType = NPCID.Snail;
       //     SpawnModBiomes = new int[1] { ModContent.GetInstance<Scenes.VerdantBiome>().Type };
        }
     
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("ne of the oldest creatures to ever migrate to the Abandoned Farm is the Grandfather Snail. This ol man has been carrying his home for 80 years!!"),
            });
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, Color.White.ToVector3() * 0.1f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail4").Type, 1f);


            }

            for (int i = 0; i < 13; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Snail, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TatteredBarrelItem>(), 3, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TatteredWood>(), 1, 1, 4));

        }
    }
}