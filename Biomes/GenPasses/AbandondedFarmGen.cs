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

namespace RealmOne.Biomes.GenPasses
{
	internal class AbandondedFarmGen : ModSystem
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
		{
			int iceBiomeGenIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Floating Islands"));
			if (iceBiomeGenIndex != -1)
				tasks.Insert(iceBiomeGenIndex - 1, new PassLegacy("Abandoned Farm", FarmBiomeGen));
		}
		bool validLocation = false;
		public void FarmBiomeGen(GenerationProgress progress, GameConfiguration config)
		{
			//bool right = WorldGen.genRand.NextBool();
			int startX = (Main.maxTilesX / 2) + WorldGen.genRand.Next(100, 150) + (int)(Main.maxTilesX / Main.maxTilesY);
			int endX = startX + WorldGen.genRand.Next(175, 225) + (int)(Main.maxTilesX / Main.maxTilesY);

			for (int i = startX; i < endX; i++)
			{
				int y = 0;
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
						if (validLocation)
						{
							WorldGen.EmptyLiquid(i, y);
							WorldGen.TileRunner(i, y, WorldGen.genRand.Next(35, 45), WorldGen.genRand.Next(10, 15), ModContent.TileType<FarmSoil>());

							if (i == startX)
                            {
								WorldGen.PlaceObject(i, y, ModContent.TileType<TractorTile>(), true);
                            }

							int rand = WorldGen.genRand.Next(1, 10);
							if (rand == 1)
                            {
								WorldGen.PlaceObject(i, y, ModContent.TileType<TractorTile>(), true);
							}
						}
						else
						{
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
		public static bool JustPressed(Keys key)
		{
			return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		}
		public override void PostUpdateWorld()
		{
			if (JustPressed(Keys.D1))
				TestMethod((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
		}

		private void TestMethod(int x, int y1)
		{
			Dust.QuickBox(new Vector2(x, y1) * 16, new Vector2(x + 1, y1 + 1) * 16, 2, Color.YellowGreen, null);
			WorldGen.PlaceObject(x, y1, ModContent.TileType<TractorTile>(), true);
		}
	}
}