using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace RealmOne.Items.Others
{
    public class FarmKey : ModItem
    {
        
            public override void SetStaticDefaults()
            {
                Item.ResearchUnlockCount = 3; 
            }

            public override void SetDefaults()
            {
                Item.CloneDefaults(ItemID.GoldenKey);
            }
        }
    
}
