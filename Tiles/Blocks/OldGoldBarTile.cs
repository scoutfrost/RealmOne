using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles.Blocks
{
    public class OldGoldBarTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1300;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(237, 222, 98), Language.GetText("MapObject.MetalBar"));
        }
    }
}