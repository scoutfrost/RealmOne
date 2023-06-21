using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class Crimcore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimcore"); 
            Tooltip.SetDefault("A bloody, dark and cold stone, dropped from the enemies that have surpassed and crumbled in the flesh");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

        }

        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 2);

        }
    }
}