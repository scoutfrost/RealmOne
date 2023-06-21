using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ID;
using RealmOne.Items.Misc.Bars;

namespace RealmOne.Items.Tools.Other
{
    public class SpiderNet  : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            ItemID.Sets.CatchingTool[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            
            Item.width = 24;
            Item.height = 28;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 40);

            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }

        public override bool? CanCatchNPC(NPC target, Player player)
        {
            // This hook is used to determine whether or not your catching tool can catch a given NPC.
            // This returns null by default, which allows vanilla to decide whether or not the NPC should be caught.
            // Returning true forces the NPC to be caught, while returning false forces the NPC to not be caught.
            // If you're unsure what to return, return null.
            // For this example, we'll give our example bug net a 20% chance to catch lava critters successfully (50% with a Warmth Potion buff active).
            if (ItemID.Sets.IsLavaBait[target.catchItem])
            {

                return false;
                
            }

            // For all cases where true isn't explicitly returned, we'll return null so that vanilla catching rules and effects can take place.
            return null;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Cobweb, 8)
                .AddIngredient(ModContent.ItemType<BrassIngot>(), 6)

                .AddTile(TileID.WorkBenches)
                .Register();
        }

    }
}
