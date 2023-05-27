using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace RealmOne.Rarities
{
	public class ModRarities : ModRarity
	{
		public override Color RarityColor => new(226, 220, 118);

		public override int GetPrefixedRarity(int offset, float valueMult)
		{
			if (offset > 0)
			{ // If the offset is 1 or 2 (a positive modifier).
				return ModContent.RarityType<HigherTierModRarity>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
			}

			return Type; // no 'lower' tier to go to, so return the type of this rarity.
		}
	}

	public class ModRarities1 : ModRarity
	{
		public override Color RarityColor => new(105, 160, 117);

		public override int GetPrefixedRarity(int offset, float valueMult)
		{
			if (offset > 0)
			{ // If the offset is 1 or 2 (a positive modifier).
				return ModContent.RarityType<PlantColor>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
			}

			return Type; // no 'lower' tier to go to, so return the type of this rarity.
		}
	}

    public class InkRarity : ModRarity
    {
        public override Color RarityColor => new(75, 35, 122);

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset > 0)
            { // If the offset is 1 or 2 (a positive modifier).
                return ModContent.RarityType<InkColor>(); // Make the rarity of items that have this rarity with a positive modifier the higher tier one.
            }

            return Type; // no 'lower' tier to go to, so return the type of this rarity.
        }
    }
}