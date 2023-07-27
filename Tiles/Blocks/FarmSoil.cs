using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ID.ContentSamples.CreativeHelper;

namespace RealmOne.Tiles.Blocks
{
    internal class FarmSoil : ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Dirt][Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;

            TileID.Sets.Grass[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;

            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Farm Soil");
            AddMapEntry(new Color(104, 156, 70), name);

            DustType = DustID.Grass;

            MineResist = 1f;

            MinPick = 20;


          

        }
    }
}