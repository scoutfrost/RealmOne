using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using RealmOne;
using RealmOne.Projectiles;
using Terraria.Localization;
using System.Drawing;
using Microsoft.Xna.Framework;
using RealmOne.Items.Misc;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Terraria.UI;
using Terraria.Audio;
using Terraria.Achievements;
using Terraria.Utilities;
using Color = Microsoft.Xna.Framework.Color;
using System.IO;
using Terraria.ModLoader.IO;
using Steamworks;
using RealmOne.Buffs;
using RealmOne.Items.Opens;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;

namespace RealmOne.RealmPlayer
{




        public class Scrolly : ModPlayer
        {
            public bool ShowScroll = false;

            public override void PostUpdate()
            {
                if (ShowScroll == true)
                {
                    var target = Main.LocalPlayer;

                }

                base.PostUpdate();
            }
        }











    //ALL THIS CODE UP TO THE # IS SPIRIT MOD'S GITHUB CODE, ALL CREDIT GOES TO THEM.
    public class ItemGlowy : ModPlayer
    {

        internal new static void Unload() => ItemGlowMask.Clear();

        public static void AddItemGlowMask(int itemType, string texturePath) => ItemGlowMask[itemType] = ModContent.Request<Texture2D>(texturePath, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        internal static readonly Dictionary<int, Texture2D> ItemGlowMask = new Dictionary<int, Texture2D>();
        public int TexturesDefaults = 0;
    }

    public class GlowMaskItemLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.ArmOverItem);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Item item = drawInfo.drawPlayer.HeldItem;

            if (item.type >= ItemID.Count && ItemGlowy.ItemGlowMask.TryGetValue(item.type, out Texture2D textureItem) && (drawInfo.drawPlayer.itemTime > 0 || item.useStyle != ItemUseStyleID.None)) //Held ItemType
                GlowMaskSystem.DrawItemGlowMask(textureItem, drawInfo);
        }
    }
//#


    public class Screenshake : ModPlayer
    {
        int timer = 0;
        public bool SmallScreenshake = false;
        bool makeTimerWork = false;



        int timer1 = 0;
        public bool BombScreenshake = false;
        bool makeTimerWork1 = false;


        int timerworm = 0;
        public bool WormScreenshake = false;
        bool WormTimerWork = false;



        int LongShakeTimer = 0;
        public bool LongShake = false;
        bool LongShakeWork= false;

        public override void ModifyScreenPosition()
        {
            //screenshake
            if (SmallScreenshake == true)
            {
                makeTimerWork = true;
            }
            if (makeTimerWork == true)
            {
                int power = 6;

                Vector2 random = new(Main.rand.Next(-power, power), Main.rand.Next(-power, power));

                timer++;
                if (timer > 0)
                {
                    Main.screenPosition += random;
                }
                if (timer >= 10)
                {
                    timer = 0;
                    makeTimerWork = false;
                }
            }



            if (BombScreenshake == true)
            {
                makeTimerWork1 = true;
            }
            if (makeTimerWork1 == true)
            {
                int power1 = 22;

                Vector2 random1 = new(Main.rand.Next(-power1, power1), Main.rand.Next(-power1, power1));

                timer1++;
                if (timer1 > 0)
                {
                    Main.screenPosition += random1;
                }
                if (timer1 >= 21)
                {
                    timer1 = 0;
                    makeTimerWork1 = false;
                }
            }

            if (WormScreenshake == true)
            {
                WormTimerWork = true;
            }
            if (WormTimerWork == true)
            {
                int powerworm = 22;

                Vector2 randomworm = new(Main.rand.Next(-powerworm, powerworm), Main.rand.Next(-powerworm, powerworm));

               timerworm++;
                if (timerworm > 0)
                {
                    Main.screenPosition += randomworm;
                }
                if (timerworm >= 21)
                {
                    timerworm = 0;
                    WormTimerWork = false;
                }
            }

            if (LongShake == true)
            {
                LongShakeWork = true;
            }
            if (LongShakeWork == true)
            {
                int longpower =11;

                Vector2 randomlong = new(Main.rand.Next(-longpower, longpower), Main.rand.Next(-longpower, longpower));

                LongShakeTimer++;
                if (LongShakeTimer > 0)
                {
                    Main.screenPosition += randomlong;
                }
                if (LongShakeTimer >= 320)
                {
                    LongShakeTimer = 0;
                    LongShakeWork = false;
                }
            }


        }


        
            //screenshake
           
            public override void ResetEffects()
            {
                if (!makeTimerWork)
                {
                    SmallScreenshake = false;
                }

            if (!makeTimerWork)
            {
                BombScreenshake = false;
            }
            if (!makeTimerWork)
            {
                WormScreenshake = false;
            }
            if (!makeTimerWork)
            {
                LongShake = false;
            }

        }
    }
    public class RealmModPlayer: ModPlayer
    {
        public bool Overseer = false;
        public bool Rusty = false;

        public override void ResetEffects()
        {
            Overseer = false;
            Rusty = false;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Rusty == true)
            {
                return Main.rand.NextFloat() >= 0.30f;
            }
            return base.CanConsumeAmmo(weapon, ammo);

        }

        

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            if (Overseer && Main.rand.NextBool(2) && !target.friendly && crit  && target.lifeMax > 10 && target.type != NPCID.TargetDummy)
            {
                Player.AddBuff(ModContent.BuffType<OverseerBuff>(), 400);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if (Overseer && Main.rand.NextBool(2) && crit && !target.friendly && target.lifeMax > 10 && !target.SpawnedFromStatue && target.type != NPCID.TargetDummy)
            {
                Player.AddBuff(ModContent.BuffType<OverseerBuff>(), 400);
            }
        }
        public class BrightProjectilePlayer : ModPlayer
        {
            public bool brightProjectiles = false;

            
        }
        public override void PreUpdate()
        {
            if (Main.GameModeInfo.IsMasterMode)
            { 

            if (Player.ZoneSkyHeight)
                {
                    Player.AddBuff(BuffID.Suffocation, 20);
                }
                if (Player.HasBuff(BuffID.Suffocation))
                {

                    CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height), new Color(80, 150, 240, 140), "You're losing air!!", false, false);
                }
            }
        }



        public override void OnEnterWorld()
        {
            
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue("Another day, Another Disappointment"), 100, 30, 250);
                

            }
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue($"Go and join the discord server for the mod!! [c/0000FF:discord.gg]"), 128, 232, 55);

            }
        }
     
        public override void OnRespawn()
        {
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue("Death is only so fragile, yet you take advantage of it."), (byte)218, (byte)39, (byte)44);

            }
        }
        public override void PlayerConnect()
        {
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue("'Your acquaintance wants to feel distress as well I see'"), (byte)64, (byte)16, (byte)227);
            }
        }
        public override void PlayerDisconnect()
        {
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue("'Never wait a second longer or shorter, it will always drive the pain towards you'"), (byte)210, (byte)30, (byte)30);
            }
        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            if (Main.netMode != 2)
            {
                Main.NewText(Language.GetTextValue("'Regeneratating is more natural and increases your cardiovascular immunity, avoid healing, you pussy'"), (byte)210, (byte)100, (byte)175);
            }
        }
        
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            
            return (IEnumerable<Item>)(object)new Item[2]
            {
                new Item(ModContent.ItemType<SpaceStarfish>(), 1, 0),
                new Item(ModContent.ItemType<BreadLoaf>(), 1, 0)
            };
        }
        
    }
}
