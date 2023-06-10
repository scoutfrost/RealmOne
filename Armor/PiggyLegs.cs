using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Legs)]

    public class PiggyLegs : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Piggy Patroller Grieves");
            Tooltip.SetDefault("15% increased fall speed but 8% decreased running speed"
                + "\n'Kicking something will probably shatter the grieves'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed -= 0.08f;
            player.maxFallSpeed += 0.15f;

        }


        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "PiggyPorcelain", 3)
            .AddTile(TileID.Furnaces)
            .Register();

        }
    }
}
