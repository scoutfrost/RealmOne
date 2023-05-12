using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

using Terraria.Audio;
using RealmOne;
using RealmOne.Common;
using RealmOne.Items;
using Terraria.Localization;
using RealmOne.Common;
using RealmOne.Common.Systems;
using RealmOne.Bosses;
using RealmOne.RealmPlayer;
using RealmOne.Rarities;
using RealmOne.NPCs.Enemies.Forest;

namespace RealmOne.BossSummons
{
    public class SquirmoSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Infested Carrot"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Awaken the sludgy scavenger of the soil'"
                + "\n'The soil will adhere relief'"
                + "\nSummons Squirmo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(246, 136, 26),
                new Color(255, 187, 119),
                new Color(243, 202, 84)
            };
        }


        public override void SetDefaults()
        {   
            Item.width = 20;
            Item.height = 20;
            Item.value = 20000;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.consumable = false;
            Item.useStyle = 2;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item2;

        }

        public override bool CanUseItem(Player player)
        {
            // If you decide to use the below UseItem code, you have to include !NPC.AnyNPCs(id), as this is also the check the server does when receiving MessageID.SpawnBoss.
            // If you want more constraints for the summon item, combine them as boolean expressions:
            //    return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<MinionBossBody>()); would mean "not daytime and no MinionBossBody currently alive"
            return !NPC.AnyNPCs(ModContent.NPCType<SquirmoHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                
                if (Main.netMode != NetmodeID.Server)
                {
                    Main.NewText(Language.GetTextValue("'The soil feels moist...'"), (byte)210, (byte)100, (byte)175);
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(255, 198, 125, 255), "'Squirmo is tunneling towards you!!", false, false);
                }
                SoundEngine.PlaySound(rorAudio.SquirmoSummonSound, player.position);

                player.GetModPlayer<Screenshake>().WormScreenshake = true;

            }

            if (player.whoAmI == Main.myPlayer)
            {
                // If the player using the item is the client
                // (explicitely excluded serverside here)
              

                int type = ModContent.NPCType<SquirmoHead>();

                int type1 = ModContent.NPCType<MegaSquirmHead>();


                int type2 = ModContent.NPCType<MegaSquirmHead>();

                int type3 = ModContent.NPCType<MegaSquirmHead>();
                int type4 = ModContent.NPCType<MegaSquirmHead>();


                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                    NPC.SpawnOnPlayer(player.whoAmI, type1);
                    NPC.SpawnOnPlayer(player.whoAmI, type2);
                    NPC.SpawnOnPlayer(player.whoAmI, type3);
                    NPC.SpawnOnPlayer(player.whoAmI, type4);

                }
                else
                {
                    // If the player is in multiplayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type1);
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type2);
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type3);
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type4);

                }
                player.GetModPlayer<Screenshake>().WormScreenshake = true;

            }
            return true;
        }
    }
}
