using RealmOne.Tiles;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RealmOne.Items.Placeables
{
    public class VinylADHD : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A.D.H.D - Kendrick Lamar");
            Tooltip.SetDefault("Track Duration: 3:35"
                +"\nNoticeable Genres: Rap, Hood Style Beats, Chill");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            // The following code links the music box's item and tile with a music track:
            //   When music with the given ID is playing, equipped music boxes have a chance to change their id to the given item type.
            //   When an item with the given item type is equipped, it will play the music that has musicSlot as its ID.
            //   When a tile with the given type and Y-frame is nearby, if its X-frame is >= 36, it will play the music that has musicSlot as its ID.
            // When getting the music slot, you should not add the file extensions!
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/ADHDDisc"), ModContent.ItemType<VinylADHD>(), ModContent.TileType<ADHDMusicBoxTile>());
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ADHDMusicBoxTile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 100000;
            Item.accessory = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "VinylADHD", "EXPLICIT SONG")
            {
                OverrideColor = new Color(240, 10, 30)

            };
            tooltips.Add(line);

        }
    }
}