﻿using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using RealmOne.Tiles.Blocks;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;
namespace RealmOne.Biomes
{
    public class BiomeTileCount : ModSystem
    {
        public int FarmCount;
      

        public static bool InFarm => ModContent.GetInstance<BiomeTileCount>().FarmCount > 50;
      
        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            FarmCount = tileCounts[ModContent.TileType<FarmSoil>()];
           
        }
    }
}
