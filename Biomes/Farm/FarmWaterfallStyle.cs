using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace RealmOne.Biomes.Farm
{
	public class FarmWaterfallStyle : ModWaterfallStyle
	{
        public override void AddLight(int i, int j) =>
        Lighting.AddLight(new Vector2(i, j).ToWorldCoordinates(), Color.White.ToVector3() * 0.2f);
    }
}