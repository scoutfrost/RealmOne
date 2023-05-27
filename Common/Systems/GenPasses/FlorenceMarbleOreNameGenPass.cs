using RealmOne.Tiles.Blocks;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace RealmOne.Common.Systems.GenPasses
{
	internal class FlorenceMarbleOreNameGenPass : GenPass
	{
		public FlorenceMarbleOreNameGenPass(string name, float loadWeight)
			: base(name, loadWeight)
		{
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
		{
			progress.Message = "Revolutionising the ancient stone";
			for (int i = 0; i < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); i++)
			{
				int num = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);
                WorldGen.TileRunner(num, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<FlorenceMarbleTile>(), false, 0f, 0f, false, true, -1);
			}
		}
	}
}
