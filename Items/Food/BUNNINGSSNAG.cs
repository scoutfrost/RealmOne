using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;


namespace RealmOne.Items.Food
{
    public class BUNNINGSSNAG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bunning's Snag!");
            Tooltip.SetDefault("''NOTHIN LIKE A GOOD OL' BUNNINGS SNAG'\r\nHeals 150HP\r\n25 minutes of increased stats" +
                "\r\n10% increased weapon speed and damage" +
                "\r\n10% increased running speed" +
                "\r\nImmunity to Poison, Weak and Confused.");

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            ItemID.Sets.IsFood[Type] = true;


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(244, 184, 64),
                new Color(151, 92, 33),
                new Color(229, 91, 122)
            };

        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = ItemRarityID.Expert;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
            Item.buffType = ModContent.BuffType<BUNNINGSBUFF>();
            Item.healLife = 150;
            Item.buffTime = 85000;
            Item.scale = 0.6f;
        }




        

    }
}