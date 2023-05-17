using RealmOne.TileEntities;
using RealmOne.Tiles;
using System.Collections.Generic;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;

namespace RealmOne.Items.Placeables
{

    public class PhotosynthesisItem : ModItem
    {
        public override void SetDefaults()
        {
            // Basically, this a just a shorthand method that will set all default values necessary to place
            // the passed in tile type; in this case, the Example Pylon tile.
            Item.DefaultToPlaceableTile(ModContent.TileType<PhotosynthesisTile1>());

            // Another shorthand method that will set the rarity and how much the item is worth.
            Item.SetShopValues(ItemRarityColor.Green2, Terraria.Item.buyPrice(gold: 10));
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photosynthesis Pillar"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Right Click to summon the Outcrop Outcast");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


        }

    }
}
