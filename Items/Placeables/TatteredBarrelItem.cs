using RealmOne.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables
{

    public class TatteredBarrelItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tattered Barrel");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<TatteredBarrel>();
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 20;
            Item.rare = 1;
            Item.value = 100000;
        }
    }
}
