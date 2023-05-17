using System;
using Terraria.Audio;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using RealmOne.Common.Systems;
using Steamworks;
using Terraria.ID;
using RealmOne.BossSummons;
using RealmOne.Pets;
using RealmOne.Potions;
using RealmOne.Items.Placeables;
using RealmOne.NPCs.Enemies;
using RealmOne.Items.Accessories;
using RealmOne.Items.Weapons.Demolitionist;
using RealmOne.Items.Weapons.Melee;
using RealmOne.Items.Weapons.Ranged;
using RealmOne.Items.Weapons.Magic;
using RealmOne.Items.Weapons.Summoner;
using RealmOne.Items.Misc;

namespace RealmOne
{
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



        public override void PostSetupContent()
        {
            Mod bossChecklist = default(Mod);
            ModLoader.TryGetMod("BossChecklist", out bossChecklist);
            if (bossChecklist != null)
            {
                bossChecklist.Call(new object[11]
                {
                    "AddBoss",
                    this,
                    "Squirmo",
                    ModContent.NPCType<SquirmoHead>(),
                    0.7f,
                    (Func<bool>)(() => DownedBossSystem.downedSquirmo),
                    (Func<bool>)(() => true),
                    new List<int>
                    {
                        ModContent.ItemType<GlobGun>(),
                        ModContent.ItemType<SquirmStaff>(),
                        ModContent.ItemType<SquirYo>(),
                        ModContent.ItemType<LoreScroll1>(),
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
                    (Func<bool>)(() => DownedBossSystem.downedOutcropOutcast),
                    (Func<bool>)(() => true),
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
                    ModContent.ItemType<Outcrop>(),
                    "To this day, the dirt titan is a remarkable standpoint of the growth of Terraria, from its hard life and from its happiest life, it will always be a symbol of the land. The name that is given to it now is called The Outcrop Outcast, for many reasons. The dirt effigy still remains on this day and is untouched even for the irresponsible. Would you rather defeat the past or lose the future?\r\n",
                    ""
                });


            }
        }
    }
}
