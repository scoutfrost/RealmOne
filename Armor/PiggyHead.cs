using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Head)]

    public class PiggyHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Piggy Patroller Mask");
            Tooltip.SetDefault("5% increased damage but 5% decreased acceleration"
                + "\n'This is so uncomfortable, but it does the job'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.runAcceleration -= 0.05f;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PiggyBody>() && legs.type == ModContent.ItemType<PiggyLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "4% increased armour penetration to all weapons" + "\nGreatly increases coin pickup"; // This is the setbonus tooltip
            player.GetArmorPenetration(DamageClass.Generic) += 4f;
            player.goldRing = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "PiggyPorcelain", 5)
            .AddTile(TileID.Furnaces)
            .Register();

        }
    }
}
