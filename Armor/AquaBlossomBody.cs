using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using RealmOne.Items.Misc.Plants;
using RealmOne.Items.Misc;

namespace RealmOne.Armor
{

    [AutoloadEquip(EquipType.Body)]
    public class AquaBlossomBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Aqua Blossom Cuirass");
            Tooltip.SetDefault("6% increases magic damage\n20+ Max Mana\n'Wet armour? No thanks.'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[((ModItem)this).Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {

            player.GetDamage(DamageClass.Magic) += 0.06f;

            player.statManaMax2 += 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ModContent.ItemType<Aquablossom>(), 6)
                                    .AddIngredient(ModContent.ItemType<WaterDriplets>(), 8)

            .AddTile(TileID.Anvils)
            .Register();

        }
    }
}
