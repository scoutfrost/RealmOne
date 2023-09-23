﻿using RealmOne.Common.Systems.GenPasses;
using RealmOne.Items.Accessories;
using RealmOne.Items.Food;
using RealmOne.Items.Misc;
using RealmOne.Items.Misc.Plants;
using RealmOne.Items.Opens;
using RealmOne.Items.Weapons.PreHM.Classless;
using RealmOne.Items.Weapons.PreHM.Grenades;
using RealmOne.Items.Weapons.PreHM.Throwing;
using RealmOne.NPCs.Critters;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.NPCs.Enemies.Underground;
using RealmOne.Tiles.Blocks;
using StructureHelper;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Common.Systems
{
    public static class BiomePlayer
    {

    }
    public class TileDrops : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (!Main.dedServ)
            {
                Player player = Main.LocalPlayer;

                if (type == 3 && Main.rand.NextBool(80))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemType<RegenMush>(), 1);
                }

                if (type == TileID.Sunflower && Main.rand.NextBool(2))
                {   
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemType<SunflowerPetal>(), 3);
                }

                if (type == TileID.Trees && Main.rand.NextBool(12) && player.ZoneCorrupt)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<CursedBerries>(), Main.rand.Next(1, 2));
                }
                if (type == TileID.Trees && Main.rand.NextBool(12) && player.ZoneCrimson)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<Goreberry>(), Main.rand.Next(1, 2));
                }
               
                if (type == TileID.Seaweed && Main.rand.NextBool(2))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<FreshSeaweed>(), Main.rand.Next(1, 4));
                }

                if (type == TileID.Coral && Main.rand.NextBool(6))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<FreshSeaweed>(), Main.rand.Next(1, 4));
                }
                if (type == 12 && Main.rand.NextBool(5))
                {
                    NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, NPCType<HeartBat>(), 1);
                }




                if (type == TileID.Dirt && DownedBossSystem.downedSquirmo == false && Main.rand.NextBool(20))
                {
                    NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, NPCType<Squirm>(), 1);
                }

                if (type == ModContent.TileType<FarmSoil>() && DownedBossSystem.downedSquirmo == false && Main.rand.NextBool(12))
                {
                    NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, NPCType<Squirm>(), 1);
                }

            }
        }


    }


  /* public sealed class SourceDependentItemTweaks : GlobalItem
    {
         public override void OnSpawn(Item item, IEntitySource source)
         {
             if (source is EntitySource_ShakeTree)
             {
                 IEntitySource newSource = item.GetSource_FromThis(); // Use a separate source for the newly created projectiles, to not cause a stack overflow.

                if (Main.dayTime == true)
                {
                    NPC.NewNPC(newSource, (int)item.position.X, (int)item.position.Y, NPCType<AcornSprinter>());
                }
             }
         }

         */

    

    public class WorldSystem : ModSystem
    {

    /*    public class Test : GenPass
        {
            public Test(string name, double loadWeight) : base(name, loadWeight)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                int x = (int)(GenVars.worldSurfaceLow + GenVars.worldSurfaceLow / 2);
                int y = (int)(GenVars.worldSurfaceLow + GenVars.worldSurfaceLow / 2);
                Point16 point = new Point16(x, y);
                Generator.GenerateStructure("Structures/Test", point, RealmOne.Instance, false);
            }

        }*/
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, (GenPass)(object)new OldGoldOreNameGenPass("OldGoldOreNameGenPass", 320f));
            }
          /*  int shiniesIndex2 = tasks.FindIndex((GenPass genpass1) => genpass1.Name.Equals("Shinies"));
            if (shiniesIndex2 != -1)
            {
                tasks.Insert(shiniesIndex2 + 1, (GenPass)(object)new FlorenceMarbleOreNameGenPass("FlorenceMarbleOreNameGenPass", 320f));
            }*/
        }
        public override void PostWorldGen()
        {
            int[] goldenchest = { ItemType<MinersPouch>() };
            int goldenchestchoice = 0;

            for (int gchestIndex = 0; gchestIndex < 1000; gchestIndex++)
            {
                Chest gchest = Main.chest[gchestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
                if (gchest != null && Main.tile[gchest.x, gchest.y].TileType == TileID.Containers && Main.tile[gchest.x, gchest.y].TileFrameX == 1 * 36)
                {
                    for (int ginventoryIndex = 0; ginventoryIndex < 40; ginventoryIndex++)
                    {
                        if (gchest.item[ginventoryIndex].type == ItemID.None)
                        {
                            gchest.item[ginventoryIndex].SetDefaults(goldenchest[goldenchestchoice]);
                            goldenchestchoice = (goldenchestchoice + 1) % goldenchest.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
            int[] acornchest = { ItemType<AcornGrenade>() };
            int acornchestchoice = 0;

            for (int acornchestIndex = 0; acornchestIndex < 1000; acornchestIndex++)
            {
                Chest acChest = Main.chest[acornchestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
                if (acChest != null && Main.tile[acChest.x, acChest.y].TileType == TileID.Containers && Main.tile[acChest.x, acChest.y].TileFrameX == 1 * 36)
                {
                    for (int acorninventory = 0; acorninventory < 40; acorninventory++)
                    {
                        if (acChest.item[acorninventory].type == ItemID.None)
                        {
                            acChest.item[acorninventory].SetDefaults(acornchest[acornchestchoice]);
                            acChest.item[acorninventory].stack = WorldGen.genRand.Next(10, 18);

                            acornchestchoice = (acornchestchoice + 1) % acornchest.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
            int[] waterchest = { ItemType<EleJelly>() };
            int waterchestchoice = 0;
            for (int WchestIndex = 0; WchestIndex < 1000; WchestIndex++)

            {

                Chest Wchest = Main.chest[WchestIndex];
                if (Wchest != null && Main.tile[Wchest.x, Wchest.y].TileType == TileID.Containers && Main.tile[Wchest.x, Wchest.y].TileFrameX == 17 * 36)
                {

                    for (int WinventoryIndex = 0; WinventoryIndex < 40; WinventoryIndex++)
                    {

                        if (Wchest.item[WinventoryIndex].type == ItemID.None)
                        {

                            Wchest.item[WinventoryIndex].SetDefaults(waterchest[waterchestchoice]);

                            Wchest.item[WinventoryIndex].stack = WorldGen.genRand.Next(20, 30);

                            waterchestchoice = (waterchestchoice + 1) % waterchest.Length;
                            //Wchest.item[WinventoryIndex].SetDefaults(Main.rand.Next(WinventoryIndex));
                            break;
                        }
                    }
                }
            }

            int[] light = { ItemType<LightbulbLiquid>() };
            int lightchoice = 0;
            for (int LchestIndex = 0; LchestIndex < 1000; LchestIndex++)

            {

                Chest lightchest = Main.chest[LchestIndex];
                if (lightchest != null && Main.tile[lightchest.x, lightchest.y].TileType == TileID.Containers && Main.tile[lightchest.x, lightchest.y].TileFrameX == 1 * 36)
                {

                    for (int LinventoryIndex = 0; LinventoryIndex < 40; LinventoryIndex++)
                    {

                        if (lightchest.item[LinventoryIndex].type == ItemID.None)
                        {

                            lightchest.item[LinventoryIndex].SetDefaults(light[lightchoice]);

                            lightchest.item[LinventoryIndex].stack = WorldGen.genRand.Next(4, 5);

                            lightchoice = (lightchoice + 1) % light.Length;
                            //chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests;
                            break;
                        }
                    }
                }
            }

            int[] pot = { ItemType<StackPotions>() };
            int potchoice = 0;
            for (int PchestIndex = 0; PchestIndex < 1000; PchestIndex++)

            {

                Chest Pchest = Main.chest[PchestIndex];
                if (Pchest != null && Main.tile[Pchest.x, Pchest.y].TileType == TileID.Containers && Main.tile[Pchest.x, Pchest.y].TileFrameX == 1 * 36)
                {

                    for (int PinventoryIndex = 0; PinventoryIndex < 40; PinventoryIndex++)
                    {

                        if (Pchest.item[PinventoryIndex].type == ItemID.None)
                        {

                            Pchest.item[PinventoryIndex].SetDefaults(pot[potchoice]);

                            Pchest.item[PinventoryIndex].stack = WorldGen.genRand.Next(2, 5);

                            potchoice = (potchoice + 1) % light.Length;
                            //chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests;
                            break;
                        }
                    }
                }
            }

            int[] itemsToPlaceInIceChests1 = { ItemType<MinersPouch>() };
            int itemsToPlaceInIceChestsChoice1 = 0;
            for (int chestIndex1 = 0; chestIndex1 < 1000; chestIndex1++)
            {
                Chest chest1 = Main.chest[chestIndex1];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
                if (chest1 != null && Main.tile[chest1.x, chest1.y].TileType == TileID.Containers && Main.tile[chest1.x, chest1.y].TileFrameX == 11 * 36)
                {
                    for (int inventoryIndex1 = 0; inventoryIndex1 < 40; inventoryIndex1++)
                    {
                        if (chest1.item[inventoryIndex1].type == ItemID.None)
                        {
                            chest1.item[inventoryIndex1].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests1));
                            itemsToPlaceInIceChestsChoice1 = (itemsToPlaceInIceChestsChoice1 + 1) % itemsToPlaceInIceChests1.Length;
                            //Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
        }
    }
}