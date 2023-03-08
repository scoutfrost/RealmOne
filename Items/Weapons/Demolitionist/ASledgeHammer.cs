using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using Terraria.Graphics;
using Terraria.Audio;
using System;
using RealmOne.Common.Systems;
using Terraria.GameContent;
using RealmOne.Common.DamageClasses;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using Terraria.DataStructures;
using RealmOne.Projectiles.Explosive;
using RealmOne.Projectiles.Sword;

namespace RealmOne.Items.Weapons.Demolitionist
{

    public class ASledgeHammer : ModItem
    {
        private int RocketDynamiteCooldown = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("All Purpose Sledgehammer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("This is super heavy..."
                + "\nSledgehammers are part of the Miner's Kit, there are 2 weapons in one"
                +"\nPrimary click is a massive overhead swing that makes a shockwave when collides on tiles or enemies"
                + "\nRight Click to throw a dynamite, swing the hammer at the dynamite to launch the dynamite towards enemies");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = 1;
            Item.knockBack = 4f;
            Item.value = 20000;
            Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
            Item.autoReuse = true;
            Item.rare = 3;
            Item.crit = 2;
            Item.useTurn = true;
            Item.hammer = 70;
            Item.shoot = ModContent.ProjectileType<ShatteredGemBladeProj2>();
            Item.shootSpeed = 10f;
            Item.noUseGraphic = true;

        }

        public override Color? GetAlpha(Color lightColor)
        {
            // Aside from SetDefaults, when making a copy of a vanilla weapon you may have to hunt down other bits of code. This code makes the item draw in full brightness when dropped.
            return Color.White;
        }

        // public override bool? UseItem(Player player)
        //   {
        //      rotation = MathHelper.ToDegrees((Main.LocalPlayer.Center - Main.MouseWorld).ToRotation());
        //     if (rotation < 0f)
        //        rotation += 360f;
        //    if (rotation >= 330f || rotation < 30f)
        //        player.AddBuff(BuffID.Swiftness, 10, true);
        ///   return true;
        //  }

        public override bool AltFunctionUse(Player Player)
        {
            return true;
        }
        
        public override bool CanUseItem(Player Player)
        {
            if (Player.altFunctionUse == 2)
            {
                Item.noUseGraphic = true;
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.useStyle = ItemUseStyleID.Swing;
             

               

                if (RocketDynamiteCooldown> 0)
                    return false;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;

                Item.useTime = 45;
                Item.useAnimation = 45;
            }

            return base.CanUseItem(Player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 dir = Vector2.Normalize(velocity) * 7;
                velocity = dir;
                type = ModContent.ProjectileType<DynamiteRocket>();
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var proj = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), position, velocity * 2, type, damage, knockback, player.whoAmI);
            proj.GetGlobalProjectile<ShatteredGemBladeProjTest>().shotFromGun = true;
            if (player.altFunctionUse == 2)
            {
            }
            return true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            SoundEngine.PlaySound(SoundID.Item70);

            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2Square(-1f, 1f);

                Dust d = Dust.NewDustPerfect(target.position, DustID.Silver, speed * 5, Scale: 2f); ;
                d.noGravity = true;

            }
        }
        public class ShatteredGemBladeProjTest : GlobalProjectile
        {
            public override bool InstancePerEntity => true;
            public bool shotFromGun = false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SilverHammer);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddIngredient(ItemID.Wood, 30);
            recipe.AddIngredient(Mod, "BrassIngot", 4);
          
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();


            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.TungstenHammer);
            recipe1.AddRecipeGroup("IronBar", 10);
            recipe1.AddIngredient(ItemID.Wood, 30);
            recipe1.AddIngredient(Mod, "BrassIngot", 4);

            recipe1.AddTile(TileID.HeavyWorkBench);
            recipe1.Register();



        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.ApprenticeStorm, 0f, 0f, 0, default, 2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;

        }


        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(6, 0);
            return offset;
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
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(76, 180, 200, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightCyan.ToVector3() * 0.5f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.BlueCrystalShard, velocity);
                dust.scale = 1.4f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }



    }
}