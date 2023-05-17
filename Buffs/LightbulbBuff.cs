using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
	public class LightbulbBuff : ModBuff
	{
		public override void SetStaticDefaults()

		{
			DisplayName.SetDefault("Lightbulb Buff");
			Description.SetDefault("'You are a lightbulb!'");

		}

		public override void Update(Player player, ref int buffIndex)
		{

		}
	}
}