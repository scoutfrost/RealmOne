using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace RealmOne.Items.Weapons.PreHM.Brass
{
    public class BrassBow : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Bow");
            Tooltip.SetDefault("Has a 20% chance of shooting a copper coin");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.damage = 11;
            Item.crit = 1;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Arrow;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.value = Item.buyPrice(copper: 80, silver: 20);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 22f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.scale = 1.2f;

        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {

            if (Main.rand.NextBool(5))
                type = ProjectileID.CopperCoin;
        }


        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(1, 0);
            return offset;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "BrassIngot", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
