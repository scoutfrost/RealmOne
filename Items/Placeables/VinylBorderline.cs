using RealmOne.Tiles;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace RealmOne.Items.Placeables
{
    public class VinylBorderline : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Borderline- Tame Impala");
            Tooltip.SetDefault("Duration: 3:58"
                +"\nNoticeable Genres: Psychedelic Rock, Alternative, Indie.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            // The following code links the music box's item and tile with a music track:
            //   When music with the given ID is playing, equipped music boxes have a chance to change their id to the given item type.
            //   When an item with the given item type is equipped, it will play the music that has musicSlot as its ID.
            //   When a tile with the given type and Y-frame is nearby, if its X-frame is >= 36, it will play the music that has musicSlot as its ID.
            // When getting the music slot, you should not add the file extensions!
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/BorderlineDisc"), ModContent.ItemType<VinylBorderline>(), ModContent.TileType<BorderlineMusicBoxTile>());
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<BorderlineMusicBoxTile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 100000;
            Item.accessory = true;
        }
    }
}