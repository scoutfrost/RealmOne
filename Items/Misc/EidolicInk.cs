using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;

namespace RealmOne.Items.Misc
{
    public class EidolicInk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eidolic Ink"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.



            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;



        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(gold: 75);
            Item.rare = ItemRarityID.Purple;
            Item.maxStack = 999;
            Item.ammo = Item.type;
            Item.consumable = true;


        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");


            line = new TooltipLine(Mod, "EidolicInk", "'Such elegant and expensive ink, use it effectively'")
            {
                OverrideColor = new Color(95, 6, 195)

            };
            tooltips.Add(line);


        }


    }
}