using RealmOne.Tiles;
using RealmOne.Tiles.Furniture;
using RealmOne.Tiles.PlantTiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables
{
    public class MiniWattleBushItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wattle Bush");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }


        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<MiniWattleBush>();
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 0, 15, 0);
        }
    }
}

