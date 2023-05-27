using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using RealmOne.Tiles;
using RealmOne.Tiles.Blocks;

namespace RealmOne.Common
{
    public class StructureThing :ModSystem
    {
        public override void PostWorldGen()
        {
            

            bool PlaceBlock(int baseCheckX, int baseCheckY)
            {
                List<int> blocksallowed = new List<int> {
                    TileID.Stone,
                    TileID.Grass,
                    TileID.Dirt,
                    TileID.ClayBlock,
                    TileID.Mud,
                };

                bool placingtheFUCKINGSTUPIDTHING = true;
                for (int i = 0; i < 3; i++) //check no obstructing blocks
                    for (int j = 0; j < 4; j++)
                    {
                        Tile tile = Framing.GetTileSafely(baseCheckX + i, baseCheckY + j);
                        if (WorldGen.SolidOrSlopedTile(tile))
                        {
                            placingtheFUCKINGSTUPIDTHING = false;
                            break;
                        }
                    }
                for (int i = 0; i < 3; i++) //check for solid foundation
                {
                    Tile tile = Framing.GetTileSafely(baseCheckX + i, baseCheckY + 4);
                    if (!WorldGen.SolidTile(tile) || !blocksallowed.Contains(tile.TileType))
                    {
                        placingtheFUCKINGSTUPIDTHING = false;
                        break;
                    }
                }

                if (placingtheFUCKINGSTUPIDTHING)
                {
                    for (int i = 0; i < 3; i++) //MAKE SURE nothing in the way
                        for (int j = 0; j < 4; j++)
                            WorldGen.KillTile(baseCheckX + i, baseCheckY + j);
                    WorldGen.PlaceTile(baseCheckX, baseCheckY + 4, ModContent.TileType<EvoGrassTile>(), false, true);
                    WorldGen.PlaceTile(baseCheckX + 1, baseCheckY + 4, ModContent.TileType<EvoGrassTile>(), false, true);
                    WorldGen.PlaceTile(baseCheckX + 2, baseCheckY + 4, ModContent.TileType<EvoGrassTile>(), false, true);
                    WorldGen.PlaceTile(baseCheckX + 2, baseCheckY + 4, ModContent.TileType<EvoGrassTile>(), false, true);

                    Tile tile = Main.tile[baseCheckX, baseCheckY + 4]; tile.Slope = 0;
                    tile = Main.tile[baseCheckX + 1, baseCheckY + 4]; tile.Slope = 0;
                    tile = Main.tile[baseCheckX + 2, baseCheckY + 4]; tile.Slope = 0;

                    WorldGen.PlaceTile(baseCheckX + 1, baseCheckY + 3, ModContent.TileType<TatteredBarrel>(), false, true);

                    return true;
                }

                return false;
            }

            int positionX = Main.spawnTileX - 1; //offset by dimensions of statue
            int positionY = Main.spawnTileY - 4;
            bool placed = false;
            for (int offsetX = -20; offsetX <= 50; offsetX++)
            {
                for (int offsetY = -20; offsetY <= 10; offsetY++)
                {
                    if (PlaceBlock(positionX + offsetX, positionY + offsetY))
                    {
                        placed = true;
                        break;
                    }
                }

                if (placed)
                    break;
            }
        }
    }
}
