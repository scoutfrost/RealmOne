using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Tiles.Blocks
{
	internal class OldGoldOreT : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;

			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileShine[Type] = 900;
			Main.tileShine2[Type] = true;
			Main.tileSpelunker[Type] = true;
			Main.tileOreFinderPriority[Type] = 350;

			LocalizedText name = CreateMapEntryName();
			name.SetDefault("Old Gold Ore");
			AddMapEntry(new Color(243, 255, 0), name);

			DustType = DustID.Gold;
			ItemDrop = ModContent.ItemType<Items.Placeables.OldGoldOre>();

			//HitSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/OldGoldTink");

			HitSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/OldGoldTink");
			MineResist = 1.5f;
			MinPick = 60;
		}
	}
}