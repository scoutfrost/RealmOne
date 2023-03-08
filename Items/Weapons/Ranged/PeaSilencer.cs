using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;
using System.Collections.Generic;
using RealmOne.Rarities;
using Terraria.Audio;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Bullet;

namespace RealmOne.Items.Weapons.Ranged
{
    public class PeaSilencer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pea Silencer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Licensed to Vanquish - the Pea Silencer is a silent pea cannon, and makes vanquishing Enemies look easy. For king and country...and Crazy Dave'");



            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 122;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = 5;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ModContent.RarityType<ModRarities1>();

            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/PeaSilencerShot");
            Item.autoReuse = true;
            Item.shootSpeed = 48f;
            Item.crit = 20;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<PeaSilencerProj>();
            Item.scale = 0.7f;

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ModContent.ProjectileType<PeaSilencerProj>(); // or ProjectileID.FireArrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DirtBlock, 50)
            .AddIngredient(ItemID.IllegalGunParts, 2)
          .AddRecipeGroup("IronBar", 28)
            .AddTile(TileID.Anvils)
            .Register();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            for (int d = 0; d < 40; d++)
                Dust.NewDust(player.position, player.width, player.height, DustID.KryptonMoss, 0f, 0f, 150, default, 1f);

            for (int i = 0; i < 10; i++)
            {
                Vector2 speed = Utils.RandomVector2(Main.rand, -10f, 1f);
                Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Grass, speed * 10, Scale: 1f); ;

                d.noGravity = true;
            }
            return true;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            else
                frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            Vector2 offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
                time = 2f - time;

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(118, 240, 209, 70), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(196, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Green.ToVector3() * 0.5f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.GreenTorch, velocity);
                dust.scale = 0.6f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }


        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(6, 0);
            return offset;
        }

    }
}