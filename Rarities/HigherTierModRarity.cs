﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Rarities
{
    public class HigherTierModRarity : ModRarity
    {
        public override Color RarityColor => new(Main.DiscoR / 2, (byte)(Main.DiscoG / 1.25f), (byte)(Main.DiscoB / 1.5f));

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            { // If the offset is -1 or -2 (a negative modifier).
                return ModContent.RarityType<ModRarities>(); // Make the rarity of items that have this rarity with a negative modifier the lower tier one.
            }

            return Type; // no 'higher' tier to go to, so return the type of this rarity.
        }
    }

    public class PlantColor : ModRarity
    {
        public override Color RarityColor => new(Main.DiscoR / 2, (byte)(Main.DiscoG / 1.25f), (byte)(Main.DiscoB / 1.5f));

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            { // If the offset is -1 or -2 (a negative modifier).
                return ModContent.RarityType<ModRarities1>(); // Make the rarity of items that have this rarity with a negative modifier the lower tier one.
            }

            return Type; // no 'higher' tier to go to, so return the type of this rarity.
        }
    }

    public class InkColor : ModRarity
    {

        public override int GetPrefixedRarity(int offset, float valueMult)
        {
            if (offset < 0)
            { // If the offset is -1 or -2 (a negative modifier).
                return ModContent.RarityType<InkRarity>(); // Make the rarity of items that have this rarity with a negative modifier the lower tier one.
            }

            return Type; // no 'higher' tier to go to, so return the type of this rarity.
        }
    }
}