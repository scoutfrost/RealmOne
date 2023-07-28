using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using RealmOne.Tiles.Blocks;

namespace RealmOne.Biomes
{
    internal class BiomeTileCount : ModSystem
    {
        public int FarmCount;
      

        public static bool InFarm => ModContent.GetInstance<BiomeTileCount>().FarmCount > 50;
      

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            //Modded biomes
            FarmCount = tileCounts[ModContent.TileType<FarmSoil>()];
           
        }
    }
}
