using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.EnemyDrops
{
    public class GoopyGrass : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goopy Grass"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Carries millions of bacteria and nutrients'"
                + "\n'#Green Thumbs'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(silver: 2);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;

        }
    }
}