using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;
using RealmOne.Common.Systems;
using Terraria.Audio;


namespace RealmOne.Tiles.Blocks
{
    internal class FlorenceMarbleTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.AllTiles[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 500;
            Main.tileShine2[Type] = true;



            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Florence Marble");
            AddMapEntry(new Color(168, 190, 152), name);

            DustType = DustID.Marble;
            ItemDrop = ModContent.ItemType<Items.Placeables.FlorenceMarble>();

            HitSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/MarbleTink");


            MineResist = 2f;
            MinPick = 50;
        }
    }
}