using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using RealmOne.Common.Core;
using RealmOne.Items.Misc;
using RealmOne.Items.Opens;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace RealmOne.RealmPlayer
{
    public static class Zones
    {
        public static bool ZoneFarmy(this Player player) 
            => player.InModBiome<Biomes.Farm.FarmSurface>();

    }
    public class Scrolly : ModPlayer
    {
        public bool ShowScroll = false;


        public override void PostUpdate()
        {
            if (ShowScroll == true)
            {
                Player target = Main.LocalPlayer;

            }




            base.PostUpdate();
        }
    }
    public class ScrollyWorm : ModPlayer

    {

        public bool ShowWorm1 = false;

        public override void PostUpdate()
        {

            if (ShowWorm1 == true)
            {
                Player target = Main.LocalPlayer;

            }


            base.PostUpdate();
        }
    }
    //ALL THIS CODE UP TO THE # IS SPIRIT MOD'S GITHUB CODE, ALL CREDIT GOES TO THEM.
    public class ItemGlowy : ModPlayer
    {

        internal new static void Unload()
        {
            ItemGlowMask.Clear();
        }

        public static void AddItemGlowMask(int itemType, string texturePath)
        {
            ItemGlowMask[itemType] = ModContent.Request<Texture2D>(texturePath, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        internal static readonly Dictionary<int, Texture2D> ItemGlowMask = new();
        public int TexturesDefaults = 0;
    }

    public class GlowMaskItemLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new BeforeParent(PlayerDrawLayers.ArmOverItem);
        }

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

        public int ScreenShake = 0;
        public int BigShake = 0;

        int timer1 = 0;
        public bool BombScreenshake = false;
        bool makeTimerWork1 = false;

        int timerworm = 0;
        public bool WormScreenshake = false;
        bool WormTimerWork = false;

        int LongShakeTimer = 0;
        public bool LongShake = false;
        bool LongShakeWork = false;

        public override void ModifyScreenPosition()
        {
            //screenshake
            if (SmallScreenshake == true)
            {
                makeTimerWork = true;
            }

            if (ScreenShake > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-3, 4), Main.rand.Next(-3, 4));
                ScreenShake--;
            }

            if (BigShake > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7));
                BigShake--;
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
                int longpower = 11;

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

    public class ThornsPlayer : ModPlayer
    {


        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (Player.HasBuff(BuffID.Thorns))

            {
                npc.AddBuff(BuffID.Poisoned, 60); // Apply Poisoned buff to the enemy for 1 second (60 frames)

            }
        }
    }
    public class RealmModPlayer : ModPlayer
    {
        public bool marbleJustJumped;

        public bool GreenNeck = false;
        public bool Overseer = false;
        public bool Rusty = false;
        public bool brassSet = false;

        public float marbleJump = 0f;

        public override void ResetEffects()
        {
            Overseer = false;
            Rusty = false;
            GreenNeck = false;
            marbleJustJumped = false;

            piggy = false;
            brassSet = false;

        }

        /*public void DoubleTapEffects(int keyDir)
		{
			if (keyDir == (Main.ReversedUpDownArmorSetBonuses ? 1 : 0))
			{
				if( brassSet && !Player.HasBuff(ModContent.BuffType<BrassMight>()))
				{
                    Player.AddBuff(ModContent.BuffType<BrassMight>(), 500);

                }
            }
		}*/
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (brassSet && Player.controlUp && Player.releaseUp & marbleJump <= 0)
            {
                Player.AddBuff(ModContent.BuffType<BrassMight>(), 400);
                SoundEngine.PlaySound(SoundID.MaxMana, Player.position);
                for (int i = 0; i < 8; i++)
                {
                    int num = Dust.NewDust(Player.position, Player.width, Player.height, DustID.BlueTorch, 0f, -2f, 0, default, 2f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
                    Main.dust[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;
                    Main.dust[num].scale *= .25f;
                    if (Main.dust[num].position != Player.Center)
                        Main.dust[num].velocity = Player.DirectionTo(Main.dust[num].position) * 6f;
                }
                marbleJump = 600;

            }

            if (Player.controlJump)
            {
                if (marbleJustJumped)
                {
                    marbleJustJumped = false;
                    if (Player.HasBuff(ModContent.BuffType<BrassMight>()))
                    {

                    }
                }
            }
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (Rusty == true)
            {
                return Main.rand.NextFloat() >= 0.25f;
            }

            return base.CanConsumeAmmo(weapon, ammo);

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (GreenNeck)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.Distance(Player.Center) < 300f && npc.lifeMax > 5 && !npc.friendly && !npc.boss)
                    {
                        npc.AddBuff(BuffID.ShadowFlame, 1000);
                    }
                }
            }
        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            if (Overseer && Main.rand.NextBool(3) && !target.friendly && hit.Crit && target.lifeMax > 10 && target.type != NPCID.TargetDummy)
            {
                Player.AddBuff(ModContent.BuffType<OverseerBuff>(), 400);
            }


        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if (Overseer && Main.rand.NextBool(3) && hit.Crit && !target.friendly && target.lifeMax > 10 && !target.SpawnedFromStatue && target.type != NPCID.TargetDummy)
            {
                Player.AddBuff(ModContent.BuffType<OverseerBuff>(), 400);
            }
        }
        public class BrightProjectilePlayer : ModPlayer
        {
            public bool brightProjectiles = false;

        }

        

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            base.OnHitByProjectile(proj, hurtInfo);
        }



        public override void PreUpdate()
        {
            

            if (!brassSet)
                marbleJustJumped = false;

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

            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("Another day, Another Disappointment"), 100, 30, 250);

            }

            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue($"[i:{ItemID.Book}]Go and join the discord server for the mod!! [c/0000FF:discord.gg/vsBJ8PrmCh] [i:{ItemID.Book}]"), 128, 232, 55);

            }
        }

        public override void OnRespawn()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("Death is only so fragile, yet you take advantage of it."), 218, 39, 44);

            }
        }
        public override void PlayerConnect()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("'Your acquaintance wants to feel distress as well I see'"), 64, 16, 227);
            }
        }
        public override void PlayerDisconnect()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("'Never wait a second longer or shorter, it will always drive the pain towards you'"), 210, 30, 30);
            }
        }

        public override void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("'Regeneratating is more natural and increases your cardiovascular immunity, avoid healing, you pussy'"), 210, 100, 175);
            }
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {

            return (IEnumerable<Item>)(object)new Item[2]
            {
                new Item(ModContent.ItemType<Suitcase>(), 1, 0),
                new Item(ModContent.ItemType<BreadLoaf>(), 1, 0),


            };
        }
    }
}
