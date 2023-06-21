using RealmOne.Tiles.Banners;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables.BannerItems
{
    public class AcornStiltB : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }
        
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Banners>() , 1);
        }
    }
}