using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RealmOne.Common.Systems
{
	public class DownedBossSystem : ModSystem
	{
		public static bool downedPiggy;

		public static bool downedSquirmo;
		public static bool downedOutcropOutcast;

		public override void OnWorldLoad()
		{
			downedPiggy = false;

			downedSquirmo = false;
			downedOutcropOutcast = false;
		}

		public override void OnWorldUnload()
		{
			downedPiggy = false;

			downedSquirmo = false;
			downedOutcropOutcast = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{

			if (downedPiggy)
			{
				tag.Set("downedPiggy", true);
			}

			if (downedSquirmo)
			{
				tag.Set("downedSquirmo", true);
			}

			if (downedOutcropOutcast)
			{
				tag.Set("downedOutcropOutcast", true);
			}
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedPiggy = tag.ContainsKey("downedPiggy");

			downedSquirmo = tag.ContainsKey("downedSquirmo");
			downedOutcropOutcast = tag.ContainsKey("downedOutcropOutcast");

		}
	}
}
