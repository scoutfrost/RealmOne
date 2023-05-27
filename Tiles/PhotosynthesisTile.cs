using Microsoft.Xna.Framework;
using RealmOne.Bosses;
using RealmOne.Common.Systems;
using RealmOne.Items.Accessories;
using RealmOne.Items.Misc;
using RealmOne.Items.Placeables;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles

{
	internal class PhotosynthesisTile1 : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolidTop[Type] = true;

			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 2;
			TileObjectData.newTile.Origin = new Point16(0, 3);
			TileObjectData.newTile.CoordinateHeights = new int[]
						{
				16,
				16,
				16,
				16
						};
			TileObjectData.addTile(Type);
			LocalizedText name = CreateMapEntryName();
			name.SetDefault("Photosynthesis Pillar");
			AddMapEntry(new Color(50, 70, 150), name);
			TileID.Sets.DisableSmartCursor[Type] = true;
			DustType = DustID.Marble;
		}

		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
		{
			return base.TileFrame(i, j, ref resetFrame, ref noBreak);
		}

		// public override bool CanKillTile(int i, int j, ref bool blockDamaged) => ;
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
		{
			offsetY = 2;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, ModContent.ItemType<PhotosynthesisItem>());
			SoundEngine.PlaySound(rorAudio.MarbleTink);
		}

		public override void MouseOver(int i, int j)
		{
			//shows the Cryptic Crystal icon while mousing over this tile
			Main.player[Main.myPlayer].cursorItemIconEnabled = true;
			Main.player[Main.myPlayer].cursorItemIconID = ModContent.ItemType<EarthEmerald>();
		}

		public override bool RightClick(int i, int j)
		{
			for (int k = 0; k < Main.npc.Length; k++)
				if (Main.npc[k].active && Main.npc[k].type == ModContent.NPCType<SquirmoHead>())
					return false;

			Player player = Main.player[Main.myPlayer];
			if (player.HasItem(ModContent.ItemType<IronGunBarrel>()))
			{
				int x = i;
				int y = j;
				while (Main.tile[x, y].TileType == Type)
					x--;
				x++;
				while (Main.tile[x, y].TileType == Type)
					y--;
				y++;
			}

			return false;
		}
	}
}