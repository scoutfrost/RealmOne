using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Bestiary;
using RealmOne.Items.Misc;
using RealmOne.Projectiles.Throwing;
using RealmOne.Items.Placeables;
using RealmOne.Common.Systems;
using RealmOne.Items.Weapons.PreHM.Throwing;

namespace RealmOne.NPCs.TownNPC
{
    [AutoloadHead]
    public class ArcheologistNPC : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Archeologist.");
            Main.npcFrameCount[NPC.type] = 26;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 1500;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 20;
            NPCID.Sets.AttackAverageChance[NPC.type] = 10;

            NPC.Happiness
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Like)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Love)
                .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Dislike)
                .SetNPCAffection(NPCID.Mechanic, AffectionLevel.Hate);
        }


        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.aiStyle = 7;
            NPC.damage = 20;
            NPC.defense = 30;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.4f;
            AnimationType = NPCID.Guide;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("An experienced and wise explorer, writing a plethora of notes of different phenomena around the land. Will give any confident and adept traveller a mission to complete his dire requests and miscellanous."),
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)/* tModPorter Suggestion: Copy the implementation of NPC.SpawnAllowed_Merchant in vanilla if you to count money, and be sure to set a flag when unlocked, so you don't count every tick. */
        {
            if (NPC.AnyNPCs(NPCType<BoundArcheologist>()))
                return false;

            for (int r = 0; r < 255; r++)
            {
                Player player = Main.player[r];
                if (player.active)
                    for (int j = 0; j < player.inventory.Length; j++)
                        if (player.inventory[j].type == ItemID.PlatinumCoin)
                            return true;
            }
            return false;
        }

        public override List<string> SetNPCNameList()
            => new() { "Frederick", "Marvin", "Charles", "Arthur", "George", "Albert", "Dens" };

        public override string GetChat()
        {
            var chatt = new List<string>
            {
                "My hand will never get tired of writing, even with frostbite!",
                "Exploring and researching is therapeutic",
                "Embrace nature, reject violence",
                "I could take on that beast again if I had the chance!",
                "I used to smoke, but I've kept all my cigarettes in my stash",
                "I sense great determination and morality in you"
            };
            return Main.rand.Next(chatt);
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Prospector Missions";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
                shop = true;
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {

            shop.item[nextSlot].SetDefaults(ItemID.FrostburnArrow, false);
            shop.item[nextSlot].value = Item.buyPrice(copper:7);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.Snowball, false);
            shop.item[nextSlot].value = Item.buyPrice(copper: 2);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.SnowballLauncher, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 3, silver: 40) ;
            nextSlot++;                                                                                                     

            shop.item[nextSlot].SetDefaults(ItemID.SnowballCannon, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 8, silver: 20);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.Rope, false);
            shop.item[nextSlot].value = Item.buyPrice(copper: 70);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.HunterPotion, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 2, silver: 50);
            nextSlot++;


            shop.item[nextSlot].SetDefaults(ItemID.TrapsightPotion, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 2, silver: 20);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.WarmthPotion, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 2);
            nextSlot++;


            shop.item[nextSlot].SetDefaults(ItemID.TeaKettle, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 3);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.Teacup, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 1, silver: 50);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.CoffeeCup, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 3);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.IceTorch, false);
            shop.item[nextSlot].value = Item.buyPrice(copper: 50);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.Campfire, false);
            shop.item[nextSlot].value = Item.buyPrice(silver: 90);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemType<StoneOvenItem>(), false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 3, silver: 22);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Parchment>(), false);
            shop.item[nextSlot].value = Item.buyPrice(silver: 80);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.Extractinator, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 3);
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.ChristmasTree, false);
            shop.item[nextSlot].value = Item.buyPrice(gold: 1);
            nextSlot++;


            if (DownedBossSystem.downedSquirmo)
            {
                shop.item[nextSlot].SetDefaults(ItemID.PlatinumBow, false);
                nextSlot++;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 10;
            knockback = 3f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 15;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileType<TundraThrowingKnifeProjectile>();
            attackDelay = 15;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 15f;
        }
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Death(), NPC.getRect(), ItemType<TundraThrowingKnife>(), 50, false, 0, false, false);
        }
    }
}
