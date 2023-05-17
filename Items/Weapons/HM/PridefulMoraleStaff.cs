using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;
using RealmOne.Common.Systems;
using RealmOne.Projectiles;
using Terraria.Audio;

namespace RealmOne.Items.Weapons.HM
{
    public class PridefulMoraleStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prideful Morale Staff");
            Tooltip.SetDefault("Shoots 3 morality projectiles");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "PridefulMoraleStaff", "'Love's gonna get you killed'")
            {
                OverrideColor = new Color(247, 21, 104)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "PridefulMoraleStaff", "'But pride's gonna be the death of you, and you and me...'")
            {
                OverrideColor = new Color(227, 106, 172)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "PridefulMoraleStaff", "First projectile is 3-5 heaven fallen magic missiles in a spread that instantly lock onto the closest enemy")
            {
                OverrideColor = new Color(200, 100, 20)

            };
            tooltips.Add(line);


            line = new TooltipLine(Mod, "PridefulMoraleStaff", "Second projectile is a fast velocity magnet ball that shoots rapidly shoots enemies")
            {
                OverrideColor = new Color(80, 200, 140)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "PridefulMoraleStaff", "Third Projectile is 3-5 high velocity aquamarine beams in a spread")
            {
                OverrideColor = new Color(140, 70, 200)

            };
            tooltips.Add(line);
        }
        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.crit = 6;
            Item.width = 38;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.rare = ItemRarityID.Red;
            Item.mana = 8;
            Item.noMelee = true;
            Item.shoot = ProjectileID.MagnetSphereBall;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/MoraleShot");
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;

        }



        public override bool OnPickup(Player player)
        {

            bool pickupText = false;
            for (int i = 0; i < 50; i++)
                if (player.inventory[i].type == ItemID.None && !pickupText)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(240, 44, 70, 255), "Damn.", false, false);
                    pickupText = true;
                }

            SoundEngine.PlaySound(rorAudio.Damn);
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            float numberProjectiles = 4 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(30);
            position += Vector2.Normalize(velocity) * 40f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.MagnetSphereBolt, damage, knockback, player.whoAmI);
            }

            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.UltraBrightTorch, 0f, 0f, 150, default, 2f);
                Dust.NewDust(player.position, player.width, player.height, DustID.BubbleBlock, 0f, 0f, 150, default, 2f);

            }
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            float numberProjectiles1 = 4 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation1 = MathHelper.ToRadians(20);
            position += Vector2.Normalize(velocity) * 40f;
            for (int i = 0; i < numberProjectiles1; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation1, rotation1, i / (numberProjectiles1 - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.MagicMissile, damage, knockback, player.whoAmI);
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
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 200, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 30, 200, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Purple.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.LunarOre, velocity);
                dust.scale = 2f;
                dust.fadeIn = 0.7f;
                dust.noGravity = true;
                dust.noLight = false;
                dust.alpha = 0;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6f, -2f);
        }
    }
}

