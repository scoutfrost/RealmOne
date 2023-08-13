using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria.IO;
using System;
using RealmOne.Tiles.Blocks;
using RealmOne.Tiles;
using StructureHelper;
using Terraria.DataStructures;
using RealmOne;
using System.Reflection.Emit;

namespace RealmOne.Biomes.GenPasses
{
	public class AbandondedFarmGen : ModSystem
	{
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
			if (genIndex != -1)
				tasks.Insert(genIndex - 1, new PassLegacy("Abandoned Farm", FarmBiomeGen));
		}
		int startX = (Main.maxTilesX / 2) + WorldGen.genRand.Next(100, 150);
        public void FarmBiomeGen(GenerationProgress progress, GameConfiguration config)
		{
            int endX = startX + WorldGen.genRand.Next(200, 250) + Main.maxTilesY / 200;
            int attempts = 0;
            bool validLocation = false;
            for (int i = startX; i < endX; i++)
			{
				int y = (int)(Main.worldSurface * 0.35f);
				while (y < Main.worldSurface)
				{
					if (WorldGen.SolidTile(i, y))
					{
						if (i == startX)
						{
							Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
							WorldUtils.Gen(new Point(startX, y + 15), new Shapes.Rectangle(endX, 30), new Actions.TileScanner(TileID.Dirt).Output(dictionary));
							int dirtCount = dictionary[TileID.Dirt];

							if (dirtCount > endX * 30 / 4)
							{
								validLocation = true;
							}
						}
						if (validLocation || attempts >= 30)
						{
							WorldGen.EmptyLiquid(i, y);
							WorldGen.TileRunner(i, y, WorldGen.genRand.Next(35, 45), WorldGen.genRand.Next(10, 15), ModContent.TileType<FarmSoil>());
						}
						else
						{
      						attempts++;
							endX -= 1;
							startX -= 1;
							i = startX;
						}
						break;
					}
					y++;
				}
			}
		}

        public override void PostWorldGen()
        {
			int barnStartX = startX + WorldGen.genRand.Next(10, 25);
			int y = 0;
			bool validLocation = false;
			int barnWidth = 83;
			int barnHeight = 41;
			while (y < Main.worldSurface)
			{
				y++;
				if (WorldGen.SolidTile(barnStartX, y) && Framing.GetTileSafely(barnStartX, y).TileType == ModContent.TileType<FarmSoil>())
				{
					validLocation = true;
					break;
				}
			}
			if (validLocation)
			{
				Generator.GenerateStructure("Structures/Barn1", new Point16(barnStartX, y - barnHeight), Mod, false);
				for (int i = barnStartX; i <= barnWidth; i++)
				{
					int j = y + barnHeight;
					while(!WorldGen.SolidTile(i, j))
					{
						j--;
						WorldGen.PlaceTile(i, j, ModContent.TileType<FarmSoil>());
					}
				}
			}
        }
	}
}

