﻿using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.Plants;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest.Wattles
{
    public class WattleBow : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wattle Bow");

            Tooltip.SetDefault("");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }



        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;

            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 11);
            Item.scale = 1f;
            Item.shoot = ModContent.ProjectileType<WattleArrowBase>();
            Item.shootSpeed = 35f;
            Item.useAmmo = AmmoID.Arrow;
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<WattleArrowBase>(); // or ProjectileID.FireArrow;
            if (Main.rand.NextBool(4))
            {
                type = ModContent.ProjectileType<WattleArrow>();
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.DarkGreen.ToVector3() * 1f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.2f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.Plantera_Green, velocity);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = false;
                dust.noLight = false;
                dust.alpha = 60;
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RichMahogany, 18)
                 .AddIngredient(ItemID.JungleSpores, 10)
                                  .AddIngredient(ItemID.Vine, 3)

                 .AddTile(TileID.Anvils)
            .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(-2, 0);
            return offset;
        }

    }
}
