using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace RealmOne.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]

    public class EmptyLocket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty Locket of The Lords"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'The internal chaos from the locket was too much for the gods to bespoke, so they abandoned it to a new rival that can withstand the essence'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {


            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.buffType = BuffID.Shine;
            Item.defense += 1;


        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 180);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.Cog, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}
