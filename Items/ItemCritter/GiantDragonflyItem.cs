using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using RealmOne.NPCs.Critters;

namespace RealmOne.Items.ItemCritter
{
    public class GiantDragonflyItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Dragonfly");

        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 99;
            Item.bait = 28;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.rare = 1;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold:1, silver: 2);
        }
        
       public override bool? UseItem(Player player)
           {
            NPC.NewNPC(player.GetSource_ItemUse(Item),(int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<GiantDragonfly>());
            return true;
        }
    }
}
