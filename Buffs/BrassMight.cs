using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
namespace RealmOne.Buffs
{
    public class BrassMight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Brass Might");
            Description.SetDefault("'The power of rust!!!'");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Terraria.ID.BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.statDefense += 10;
            player.moveSpeed -= 0.14f;


        }

    }
}
