using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Other;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Piggy
{
    public class CoindropWand : ModItem
    {
        bool set = false;
        Vector2 mouse;
        int coin = 0;
        int triggerAmount = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coindrop Wand");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 36;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(0, 0, 87, 34);
            Item.rare = ItemRarityID.Yellow;
            Item.useTurn = true;
            Item.crit = 10;
            Item.UseSound = SoundID.Item30;
        }

        public override bool? UseItem(Player p)
        {
            if (coin > 0)
            {
                coin--;
            }

            if (set == false)
            {
                set = true;
                mouse = Main.MouseWorld;
            }
            for (int i = 0; i < 6; i++)
            {
                if (coin == 0)
                {
                    coin = 8;
                    triggerAmount++;

                    SoundEngine.PlaySound(SoundID.Item9, p.position);
                    Vector2 SpawnLoc = new Vector2(mouse.X - 96, mouse.Y - 900);
                    int select = Main.rand.Next(1, 5);
                    if (select == 1)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 193), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<PlatinumCoinFriendly>(), Item.damage, 6f, Main.myPlayer);
                    }
                    if (select == 2)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 193), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<GoldCoinFriendly>(), Item.damage, 6f, Main.myPlayer);
                    }
                    if (select == 3)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 193), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<SilverCoinFriendly>(), Item.damage, 6f, Main.myPlayer);
                    }
                    if (select == 4)
                    {
                        Projectile.NewProjectile(p.GetSource_FromThis(), new Vector2(SpawnLoc.X + Main.rand.Next(1, 193), SpawnLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<CopperCoinFriendly>(), Item.damage, 6f, Main.myPlayer);
                    }
                }

            }


            set = false;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod, "PiggyPorcelain", 5);
            recipe.AddIngredient(ItemID.GoldCoin, 1);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}