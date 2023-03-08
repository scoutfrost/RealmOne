using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using RealmOne.Items.Misc;

namespace RealmOne.NPCs.TownNPC
{
    public class BoundArcheologist : ModNPC
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowbanked Archeologist");
            NPCID.Sets.TownCritter[NPC.type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.townNPC = true;
            NPC.dontTakeDamage = true;
            NPC.width = 32;
            NPC.height = 48;
            NPC.aiStyle = 0;
            NPC.damage = 0;
            NPC.defense = 20;
            NPC.lifeMax = 10000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.rarity = 1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            //If any player is underground and has an example item in their inventory, the example bone merchant will have a slight chance to spawn.
            if (spawnInfo.Player.ZoneSnow && spawnInfo.Player.inventory.Any(item => item.type == ModContent.ItemType<LoreScroll1>()))
            {
                return 0.1f;
            }
            
            //Else, the example bone merchant will not spawn if the above conditions are not met.
            return 0f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

        public override string GetChat() => "Dear God, I thought I was gonna get permanent frostbite if you didn't come to my rescue! Thank you kind lad, now excuse me but do you a kettle and tea? That would surely warm the old and frigid hands of mine right up! ";

        public override void AI()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.homeless = false;
                NPC.homeTileX = -1;
                NPC.homeTileY = -1;
                NPC.netUpdate = true;
            }

            if (NPC.wet)
                NPC.life = 250;

            foreach (var player in Main.player)
            {
                if (!player.active)
                    continue;

                if (player.talkNPC == NPC.whoAmI)
                {
                    Rescue();
                    return;
                }
            }
        }

        public void Rescue()
        {
            NPC.Transform(NPCType<ArcheologistNPC>());
            NPC.dontTakeDamage = false;
        }
    }
}
