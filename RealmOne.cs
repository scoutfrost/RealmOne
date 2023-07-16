using RealmOne.Bosses;
using RealmOne.Common.Systems;
using RealmOne.Items.Accessories;
using RealmOne.Items.BossSummons;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Others;
using RealmOne.Items.PaperUI;
using RealmOne.Items.Placeables;
using RealmOne.Items.Potions;
using RealmOne.Items.Weapons.Demolitionist;
using RealmOne.Items.Weapons.PreHM.BossDrops.OutcastDrops;
using RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops;
using RealmOne.Items.Weapons.Summoner;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.NPCs.Enemies.MiniBoss;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ModLoader;

namespace RealmOne
{
    //Test
    public class RealmOne : Mod
    {
        public const string AssetPath = $"{nameof(RealmOne)}/Assets/";

        public static float ModTime { get; internal set; }
        public static object MessageType { get; internal set; }

        internal static object GetLegacySoundSlot(object custom, string v)
        {
            throw new NotImplementedException();
        }

        internal static object GetLegacySoundSlot(SoundType soundType)
        {
            throw new NotImplementedException();
        }
        
      /*  public override void PostSetupContent()
        {
            ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist);
            if (bossChecklist != null)
            {
                bossChecklist.Call(new object[11]
                {

                    "AddBoss",
                    this,
                    "Squirmo",
                    ModContent.NPCType<SquirmoHead>(),
                    0.7f,
                    () => DownedBossSystem.downedSquirmo,
                    () => true,
                    new List<int>
                    {
                        ModContent.ItemType<GlobGun>(),
                        ModContent.ItemType<SquirmStaff>(),
                        ModContent.ItemType<SquirYo>(),
                        ModContent.ItemType<SquirmoLorePageOne>(),
                        ModContent.ItemType<TwinklingTwig>(),
                    },
                    ModContent.ItemType<SquirmoSummon>(),
                    "Even from the past dread of worm adaptation, they havent really caused global damage. But for Squirmo, ever seeking revenge on human inhabitants is still a current warning for people.  Adhere the relief of the soil by defeating Squirmo!!",
                    ""

                });

                bossChecklist.Call(new object[11]
                {
                    "AddBoss",
                    this,
                    "The Outcrop Outcast",
                    ModContent.NPCType<MossyMarauder>(),
                    3f,
                    () => DownedBossSystem.downedOutcropOutcast,
                    () => true,
                    new List<int>
                    {

                        ModContent.ItemType<EarthEmerald>(),
                        ModContent.ItemType<TheOutcastsOverseer>(),
                        ModContent.ItemType<TheCalender>(),
                        ModContent.ItemType<BarrenBrew>(),
                        ModContent.ItemType<BotanicLogLauncher>(),
                        ModContent.ItemType<Overgrowth>(),
                        ModContent.ItemType<FoliageFury>(),

                    },
                    ModContent.ItemType<PhotosynthesisItem>(),
                    "To this day, the dirt titan is a remarkable standpoint of the growth of Terraria, from its hard life and from its happiest life, it will always be a symbol of the land. The name that is given to it now is called The Outcrop Outcast, for many reasons. The dirt effigy still remains on this day and is untouched even for the irresponsible. Would you rather defeat the past or lose the future?\r\n",
                    ""
                });
                bossChecklist.Call(new object[11]
             {
                "AddBoss",
                this,
                "Possessed Piggy Bank",
                ModContent.NPCType<PossessedPiggy>(),
                0.3f,
                (Func<bool>)(() => DownedBossSystem.downedPiggy),
                (Func<bool>)(() => true),
                new List<int> { ModContent.ItemType<PiggyPorcelain>() },
                ModContent.ItemType<MoneyVase>(),
                "A rare scavenger of the land, looking for any Terrarian to stumble across it, stealing all its loot!!",
                ""
               });
            }
        }*/
    }
}
