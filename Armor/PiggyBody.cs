using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Body)]

    public class PiggyBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Piggy Patroller Bodyplate");
            Tooltip.SetDefault("5% increased knockback but 8% decreased movement speed"
                + "\nDiscount on all Shop Items!"
                + "\n'Carrying a heavy bodyplate full of porcelain'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Generic) += 0.05f;
            player.moveSpeed -= 0.08f;
            player.discountAvailable = true;

        }


        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "PiggyPorcelain", 6)
            .AddTile(TileID.Furnaces)
            .Register();

        }
    }
}
