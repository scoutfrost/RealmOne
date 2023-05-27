using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Tiles.Blocks
{
	internal class EvoGrassTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMerge[Type][Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileMerge[TileID.Dirt][Type] = true;

			TileID.Sets.Grass[Type] = true;
			TileID.Sets.Conversion.Grass[Type] = true;

			LocalizedText name = CreateMapEntryName();
			name.SetDefault("Evo Grass");
			AddMapEntry(new Color(90, 127, 78), name);

			DustType = DustID.Grass;

			MineResist = 1f;
			MinPick = 20;
		}
	}
}