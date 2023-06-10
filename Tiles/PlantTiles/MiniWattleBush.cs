using Microsoft.Xna.Framework;
using RealmOne.Items.Placeables;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles.PlantTiles
{
    public class MiniWattleBush : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLighted[Type] = true;

            HitSound = SoundID.Grass;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);

            MineResist = 1f;
            MinPick = 20;

            DustType = DustID.Sunflower;
            LocalizedText name = CreateMapEntryName(); //Name is in the localization file
            name.SetDefault("Mini Wattle Bush");

            AddMapEntry(new Color(215, 240, 60));
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.TileFrameY <= 18 && (tile.TileFrameX <= 36 || tile.TileFrameX >= 72))
            {
                r = 0.6f;
                g = 0.7f;
                b = 0.15f;
            }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<MiniWattleBushItem>());
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<Items.Misc.Plants.Wattle>(), Main.rand.Next(2, 4));

            if (Main.netMode != NetmodeID.Server)
            {

                int WattleGore1 = Mod.Find<ModGore>("WattleGore1").Type;
                int WattleGore2 = Mod.Find<ModGore>("WattleGore2").Type;


                var entitySource = new EntitySource_TileBreak(i, j);

                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers


                for (int g = 0; g < 1; g++)
                {
                    Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(1)), WattleGore1);
                    Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(1)), WattleGore2);

                }
            }


        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }

    }
}
