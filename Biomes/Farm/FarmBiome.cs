using Microsoft.Xna.Framework;
using RealmOne;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;
using System;
using Terraria.ModLoader.Utilities;
using Terraria.WorldBuilding;

namespace RealmOne.Biomes.Farm
{
    public class FarmSurface : ModBiome
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Abandoned Farm");
       public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("RealmOne/Biomes/Farm/FarmWaterfallStyle");
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("");
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override int Music => Main.LocalPlayer.townNPCs >= 2 ? -1 : (Main.dayTime ? MusicLoader.GetMusicSlot(Mod, "Assets/Music/CottageOrchestra") : MusicLoader.GetMusicSlot(Mod, "Assets/Music"));

        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => MapBackground;
        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => "";

    /*    public override bool IsBiomeActive(Player player)
        {
            bool surface = player.ZoneSkyHeight || player.ZoneOverworldHeight;
            return BiomeTileCount.InFarm&& surface;
        }*/





    }
}
