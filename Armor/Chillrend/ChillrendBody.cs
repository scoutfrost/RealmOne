using RealmOne.Items.Misc;
using RealmOne.Items.Misc.Plants;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{

    [AutoloadEquip(EquipType.Body)]
    public class ChillrendBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Chillrend Breastplate");
            Tooltip.SetDefault("6% increases ranged damage\n6% increased ranged speed'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[((ModItem)this).Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {

            player.GetDamage(DamageClass.Ranged) += 0.06f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.06f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.IceBlock, 16)
                                    .AddIngredient(ModContent.ItemType<WaterDriplets>(), 8)

            .AddTile(TileID.Anvils)
            .Register();

        }
    }
}
